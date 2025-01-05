using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RevokeAllUsers;
public record RevokeAllUsersCommand(
    RevokeAllUsersDto Dto) : ICommand;
