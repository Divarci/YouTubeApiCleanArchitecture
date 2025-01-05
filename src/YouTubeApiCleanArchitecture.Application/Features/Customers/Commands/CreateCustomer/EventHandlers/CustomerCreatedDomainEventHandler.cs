using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using YouTubeApiCleanArchitecture.Application.Abstraction.Emailing;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.CreateCustomer.EventHandlers;
internal sealed class CustomerCreatedDomainEventHandler(
    ILogger<CustomerCreatedDomainEventHandler> logger,
    IUnitOfWork unitOfWork,
    IEmailService emailService) : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly ILogger<CustomerCreatedDomainEventHandler> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(
        CustomerCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var eventName = typeof(CustomerCreatedDomainEvent).Name;

        _logger.LogInformation("Executing event {EventName}", eventName);

        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(notification.CustomerId, cancellationToken);

        if (customer is null)
        {
            using (LogContext.PushProperty("Error", "Customer not exist", true))
            {
                _logger.LogError("Event {EventName} not executed", eventName);
            }

            return;
        }

        await _emailService.SendAsync();

        _logger.LogInformation("Event {EventName} processed successfully", eventName);
    }
}
