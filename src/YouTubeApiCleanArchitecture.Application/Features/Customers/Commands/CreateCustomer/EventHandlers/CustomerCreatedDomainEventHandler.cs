using MediatR;
using YouTubeApiCleanArchitecture.Application.Abstraction.Emailing;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.CreateCustomer.EventHandlers;
internal sealed class CustomerCreatedDomainEventHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService) : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(
        CustomerCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(notification.CustomerId, cancellationToken);

        if (customer is null)
            return; // Search

        await _emailService.SendAsync();
    }
}
