using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Infrastructure.Outbox;

[DisallowConcurrentExecution] //post one event in one transaction
internal sealed class ProcessOutboxMessagesJobs(
    IPublisher publisher,
    ILogger<ProcessOutboxMessagesJobs> logger,
    IOptions<OutboxOptions> outboxOptions,
    AppDbContext context) : IJob
{
    private static readonly JsonSerializerSettings JsonSerialozerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    private readonly IPublisher _publisher = publisher;
    private readonly ILogger<ProcessOutboxMessagesJobs> _logger = logger;
    private readonly OutboxOptions _outboxOptions = outboxOptions.Value;
    private readonly AppDbContext _dBcontext = context;

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Beginning to process outbox messages");

        await using var transaction = await _dBcontext.Database.BeginTransactionAsync();

        try
        {
            var outboxMessages = await GetOutboxMessagesAsync(_dBcontext);

            foreach (var outboxMessage in outboxMessages)
            {
                Exception? exception = null;

                try
                {
                    var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                        outboxMessage.Content,
                        JsonSerialozerSettings);

                    if (domainEvent is null)
                        throw new ArgumentNullException(nameof(domainEvent)); //change

                    await _publisher.Publish(domainEvent, context.CancellationToken);
                }
                catch (Exception caughtException)
                {
                    _logger.LogError(
                        caughtException,
                        "Exception while processing outbox message {MessageId}",
                        outboxMessage.Id);

                    exception = caughtException;
                }

                await UpdateOutboxMessageAsync(_dBcontext, outboxMessage, exception);
            }

            await _dBcontext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            _logger.LogError(ex, "Exception while processing outbox messages, All rollbacked");

            throw new Exception();
        }
    }

    private async Task<IReadOnlyList<OutboxMessagesResponse>> GetOutboxMessagesAsync(
        AppDbContext context)
    {
        var outboxMessages = await context
            .OutboxMessages
            .FromSqlRaw("""
                SELECT TOP (@BatchSize) Id, Content 
                FROM OutboxMessages WITH (UPDLOCK) 
                WHERE ProcessedOnUtc IS NULL OR Error IS NOT NULL
                ORDER BY OccuredOnUtc 
            """, new SqlParameter("@BatchSize", _outboxOptions.BatchSize))
            .Select(x => new OutboxMessagesResponse(x.Id, x.Content))
            .ToListAsync();

        return outboxMessages;
    }

    private async Task UpdateOutboxMessageAsync(
        AppDbContext context,
        OutboxMessagesResponse outboxMessageResponse,
        Exception? exception)
    {
        var outboxMessage = await context.OutboxMessages
            .FindAsync(outboxMessageResponse.Id);

        outboxMessage!.Update(DateTime.UtcNow, exception?.ToString()!);
    }

    internal sealed record OutboxMessagesResponse(Guid Id, string Content);
}
