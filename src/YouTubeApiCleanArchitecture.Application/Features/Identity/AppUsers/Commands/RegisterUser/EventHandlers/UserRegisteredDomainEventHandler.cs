using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Roles;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.Events;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RegisterUser.EventHandlers;
internal sealed class UserRegisteredDomainEventHandler(
    ILogger<UserRegisteredDomainEventHandler> logger,
    UserManager<AppUser> userManager)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly ILogger<UserRegisteredDomainEventHandler> _logger = logger;
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task Handle(
        UserRegisteredDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var eventName = typeof(UserRegisteredDomainEvent).Name;

        _logger.LogInformation("Executing event {EventName}", eventName);  

        var user = await _userManager
            .FindByIdAsync(notification.UserId.ToString());

        if (user is null)
        {
            using (LogContext.PushProperty("Error", "User not exist", true))
            {
                _logger.LogError("Event {EventName} not executed", eventName);
            }

            return;
        }

        if (string.IsNullOrEmpty(notification.AdminKey))        
            await _userManager.AddToRoleAsync(user, AppRole.User.Name!);
        else
        {
            if (!notification.AdminKey.Equals(AppRole.ADMIN_KEY))
                throw new AdminKeyNotMatchException(
                    ["Admin key not matched"]);

            await _userManager.AddToRoleAsync(user, AppRole.Admin.Name!);
        }

        _logger.LogInformation("Event {EventName} processed successfully", eventName);
    }
}
