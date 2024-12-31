namespace YouTubeApiCleanArchitecture.Domain.Abstraction;

public interface IDomainEventRaiser
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
    void RaiseDomainEvent(IDomainEvent domainEvent);
}