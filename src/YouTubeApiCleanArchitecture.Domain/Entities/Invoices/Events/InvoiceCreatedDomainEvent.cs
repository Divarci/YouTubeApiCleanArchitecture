using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
public record InvoiceCreatedDomainEvent(Guid InvoiceId) : IDomainEvent;