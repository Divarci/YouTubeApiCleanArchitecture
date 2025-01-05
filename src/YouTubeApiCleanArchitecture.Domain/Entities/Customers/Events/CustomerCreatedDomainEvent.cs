using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
public record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
