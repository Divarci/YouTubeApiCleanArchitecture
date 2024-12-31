using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RefreshToken;
public record RefreshTokenCommand(
    RefreshTokenDto Dto) : ICommand<RefreshTokenResponse>;