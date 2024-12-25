using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
using YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetProduct;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetCustomer;
internal sealed class GetCustomerQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQueryHandler<GetCustomerQuery, CustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponse>> Handle(
        GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
            return Result<CustomerResponse>
                .Failed(400, "Null.Error", $"The customer with the id: {request.CustomerId} not exist");

        var response = _mapper.Map<CustomerResponse>(customer);

        return Result<CustomerResponse>
            .Success(response, 200);
    }
}
