using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.LoginUser;
using YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RefreshToken;
using YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RegisterUser;
using YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RevokeAllUsers;
using YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RevokeUser;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;

namespace YouTubeApiCleanArchitecture.API.Controllers.Version1.Authentication;

[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController(
    ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(
        RegisterUserDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new RegisterUserCommand(request),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(
        LoginUserDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new LoginUserCommand(request),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> GetAccessTokenByRefreshTokenAsync(
        RefreshTokenDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new RefreshTokenCommand(request),
            cancellationToken);

        return CreateResult(response);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost("RevokeUser")]
    public async Task<IActionResult> RevokeUserAsync(
        RevokeUserDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new RevokeUserCommand(request),
            cancellationToken);

        return CreateResult(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("RevokeAll")]
    public async Task<IActionResult> RevokeAllAsync(
        RevokeAllUsersDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new RevokeAllUsersCommand(request),
            cancellationToken);

        return CreateResult(response);
    }
}
