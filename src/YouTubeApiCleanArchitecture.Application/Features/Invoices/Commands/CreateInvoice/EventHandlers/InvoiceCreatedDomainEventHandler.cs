using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.CreateInvoice.EventHandlers;
internal sealed class InvoiceCreatedDomainEventHandler(
    ILogger<InvoiceCreatedDomainEventHandler> logger,
    IUnitOfWork unitOfWork) : INotificationHandler<InvoiceCreatedDomainEvent>
{
    private readonly ILogger<InvoiceCreatedDomainEventHandler> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        InvoiceCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var eventName = typeof(InvoiceCreatedDomainEvent).Name;

        _logger.LogInformation("Executing event {EventName}", eventName);

        var invoice = await _unitOfWork.Repository<Invoice>()
            .GetAsync(
                enableTracking: true,
                predicates: [x => x.Id == notification.InvoiceId],
                cancellationToken: cancellationToken,
                includes: [x => x.Include(x => x.Customer)]);

        if (invoice is null)
        {
            using (LogContext.PushProperty("Error", "Invoice not exist", true))
            {
                _logger.LogError("Event {EventName} not executed", eventName);
            }

            return;
        }

        invoice.Customer.IncreaseBalance(invoice.TotalBalance);

        _unitOfWork.Repository<Invoice>().Update(invoice);

        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Event {EventName} processed successfully", eventName);
    }
}
