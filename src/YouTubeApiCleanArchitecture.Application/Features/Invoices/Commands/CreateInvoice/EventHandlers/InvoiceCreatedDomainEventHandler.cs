using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.CreateInvoice.EventHandlers;
internal sealed class InvoiceCreatedDomainEventHandler(
    IUnitOfWork unitOfWork) : INotificationHandler<InvoiceCreatedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(
        InvoiceCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>()
            .GetAll()
            .Include(x=>x.Customer)
            .FirstOrDefaultAsync(x=>x.Id == notification.InvoiceId);

        if (invoice is null)
            return; // Search

        invoice.Customer.UpdateBalance(invoice.TotalBalance);

        _unitOfWork.Repository<Invoice>().Update(invoice);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
