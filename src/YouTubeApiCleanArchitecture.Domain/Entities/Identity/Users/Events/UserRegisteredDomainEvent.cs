using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.Events;
public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string? AdminKey) : IDomainEvent;
