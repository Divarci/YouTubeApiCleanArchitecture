using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.RemoveInvoice.Events;
internal sealed class InvoiceRemovedDomainEventHandler(
    ILogger<InvoiceRemovedDomainEventHandler> logger,
    IUnitOfWork unitOfWork)
    : INotificationHandler<InvoiceRemovedDomainEvent>
{
    private readonly ILogger<InvoiceRemovedDomainEventHandler> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        InvoiceRemovedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var eventName = typeof(InvoiceRemovedDomainEvent).Name;

        _logger.LogInformation("Executing event {EventName}", eventName);

        var customer = await _unitOfWork.Repository<Customer>()
            .GetAsync(
                enableTracking: true,
                predicates: [x => x.Id == notification.CustomerId],
                cancellationToken: cancellationToken);

        if (customer is null)
        {
            using (LogContext.PushProperty("Error", "Customer not exist", true))
            {
                _logger.LogError("Event {EventName} not executed", eventName);
            }

            return;
        }

        customer.DecreaseBalance(notification.InvoiceAmount);

        _unitOfWork.Repository<Customer>().Update(customer);

        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Event {EventName} processed successfully", eventName);
    }
}
