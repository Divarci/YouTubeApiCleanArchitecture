using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;

namespace YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
public abstract class BaseEntity : IDomainEventRaiser
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity() { }

    protected BaseEntity(Guid id)
        => Id = id;

    public Guid Id { get; init; }

    public byte[] RowVersion { get; set; } = null!;


    public IReadOnlyList<IDomainEvent> GetDomainEvents()
       => _domainEvents.ToList();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}
