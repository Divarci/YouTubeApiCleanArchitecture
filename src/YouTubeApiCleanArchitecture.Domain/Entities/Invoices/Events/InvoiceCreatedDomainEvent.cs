using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
public sealed record InvoiceCreatedDomainEvent(
    Guid InvoiceId) : IDomainEvent;