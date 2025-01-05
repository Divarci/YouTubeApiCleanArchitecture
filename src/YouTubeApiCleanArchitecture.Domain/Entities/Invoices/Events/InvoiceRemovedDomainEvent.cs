using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
public record InvoiceRemovedDomainEvent(
    Guid CustomerId, 
    decimal InvoiceBalance) : IDomainEvent;
