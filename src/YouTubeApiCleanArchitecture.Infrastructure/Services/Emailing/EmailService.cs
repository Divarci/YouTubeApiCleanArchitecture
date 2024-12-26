using YouTubeApiCleanArchitecture.Application.Abstraction.Emailing;

namespace YouTubeApiCleanArchitecture.Infrastructure.Services.Emailing;
public class EmailService : IEmailService
{
    public Task SendAsync()
    {
        return Task.CompletedTask;
    }
}
