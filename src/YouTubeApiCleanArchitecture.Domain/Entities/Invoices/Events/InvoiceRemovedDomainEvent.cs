using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
public record InvoiceRemovedDomainEvent(
    Guid CustomerId,
    decimal InvoiceAmount): IDomainEvent;
