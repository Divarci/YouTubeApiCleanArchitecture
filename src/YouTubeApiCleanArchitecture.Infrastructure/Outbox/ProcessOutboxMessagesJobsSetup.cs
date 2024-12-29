using Microsoft.Extensions.Options;
using Quartz;

namespace YouTubeApiCleanArchitecture.Infrastructure.Outbox;
internal class ProcessOutboxMessagesJobsSetup(
    IOptions<OutboxOptions> outboxOptions) : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _outboxOptions = outboxOptions.Value;

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessOutboxMessagesJobs);

        options
            .AddJob<ProcessOutboxMessagesJobs>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(
                            _outboxOptions.IntervalInSeconds).RepeatForever()));
    }
}
