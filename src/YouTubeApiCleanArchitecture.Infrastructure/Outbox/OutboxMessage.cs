namespace YouTubeApiCleanArchitecture.Infrastructure.Outbox;
public sealed class OutboxMessage
{
    public OutboxMessage(
        Guid id,
        DateTime occuredOnUtc,
        string type,
        string content)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
        Type = type;
        Content = content;
    }

    public Guid Id { get; private set; }
    public DateTime OccuredOnUtc { get; private set; }
    public string Type { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    public void Update(DateTime processedOnUtc, string error)
    {
        ProcessedOnUtc = processedOnUtc;
        Error = error;
    }
}
