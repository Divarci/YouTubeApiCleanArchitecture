using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
public record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
