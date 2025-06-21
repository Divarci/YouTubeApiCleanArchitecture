using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Shared;
public class BaseTest
{
    protected TEvent HandleRaiseDomainEvent<TEvent>(BaseEntity entity)
        => entity
            .GetDomainEvents()
            .OfType<TEvent>()
            .SingleOrDefault() ??
            throw new ArgumentNullException("Domain Event not found");
}
