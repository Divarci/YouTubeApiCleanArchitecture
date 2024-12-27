using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.UpdateCustomer;
internal sealed class UpdateCustomerCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
            return Result<NoContentDto>
                .Failed(400, "Null.Error", $"The customer with the id: {request.CustomerId} not exist");

        customer.Update(request.Dto);

        _unitOfWork.Repository<Customer>()
            .Update(customer);

        await _unitOfWork.CommitAsync(
            cancellationToken, 
            checkForConcurrency: true);

        return Result<NoContentDto>
            .Success(204);
    }
}
