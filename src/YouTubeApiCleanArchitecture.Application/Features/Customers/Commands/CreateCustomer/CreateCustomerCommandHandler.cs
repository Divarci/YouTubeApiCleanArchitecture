using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.CreateCustomer;
internal sealed class CreateCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateCustomerCommand, CustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponse>> Handle(
        CreateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.Dto, Guid.NewGuid());

        await _unitOfWork.Repository<Customer>()
            .CreateAsync(customer);

        await _unitOfWork.CommitAsync(cancellationToken);

        var response = _mapper.Map<CustomerResponse>(customer);

        return Result<CustomerResponse>
            .Success(response, 201);
    }
}
