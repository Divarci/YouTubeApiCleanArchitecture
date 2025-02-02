using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
public sealed record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
