using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Application.Abstraction.TokenProviding;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Roles;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.LoginUser;
internal sealed class LoginUserCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService) : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<LoginUserResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByEmailAsync(request.Dto.Email);

        if (user is null)
            return Result<LoginUserResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "LoginFailed.Error",
                    ErrorMessages = ["Email or password not match"]
                });

        var isValidPassword = await _userManager
            .CheckPasswordAsync(user, request.Dto.Password);

        if (!isValidPassword)
            return Result<LoginUserResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "LoginFailed.Error",
                    ErrorMessages = ["Email or password not match"]
                });

        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = await _tokenService.CreateTokenAsync(user, roles);

        var refreshToken = _tokenService.GenerateRefreshToken();

        user.AddRefreshTokenInfo(
            refreshToken,
            _tokenService.GetRefreshTokenExpireDate());

        await _userManager.UpdateAsync(user);
        await _userManager.UpdateSecurityStampAsync(user);

        var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

        var response = new LoginUserResponse
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            ExpireDate = accessToken.ValidTo
        };

        return Result<LoginUserResponse>
            .Success(response, 200);
    }
}
