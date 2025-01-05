using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetAllCustomers;
internal sealed class GetAllCustomersQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQueryHandler<GetAllCustomersQuery, CustomerResponseCollection>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponseCollection>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Repository<Customer>()
            .GetAllAsync<CustomerResponse>(
                enableTracking: false,
                mapper: _mapper, 
                cancellationToken: cancellationToken);

        var response = new CustomerResponseCollection
        {
            Customers = customers.AsReadOnly()
        };

        return Result<CustomerResponseCollection>
            .Success(response,200);
    }
}
