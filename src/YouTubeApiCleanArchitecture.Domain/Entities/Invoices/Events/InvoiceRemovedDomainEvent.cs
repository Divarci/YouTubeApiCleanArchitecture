using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;

public record InvoiceRemovedDomainEvent(
    Guid CustomerId,
    Money InvoiceValue) : IDomainEvent;