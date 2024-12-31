using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RegisterUser;
public record RegisterUserCommand(
    RegisterUserDto Dto) : ICommand<RegisterUserResponse>;
