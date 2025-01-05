using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Application.Abstraction.TokenProviding;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RefreshToken;
internal sealed class RefreshTokenCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<RefreshTokenResponse>> Handle(
        RefreshTokenCommand request, 
        CancellationToken cancellationToken)
    {
        var principal = _tokenService
            .GetPrincipalFromExpiredToken(request.Dto.AccessToken);

        var email = principal.FindFirstValue(ClaimTypes.Email);

        if (email is null)
            return Result<RefreshTokenResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "RefreshTokenFail.Error",
                    ErrorMessages = ["Token not contains email"]
                });

        var user = await _userManager.FindByEmailAsync(email);
        
        if( user is null)
            return Result<RefreshTokenResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "RefreshTokenFail.Error",
                    ErrorMessages = ["User not exist"]
                });

        var roles = await _userManager.GetRolesAsync(user);

        if (roles is null || roles.Count == 0)
            return Result<RefreshTokenResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "RefreshTokenFail.Error",
                    ErrorMessages = ["User don't have any role"]
                });

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenExpireDate = _tokenService.GetRefreshTokenExpireDate();

        user.UpdateRefreshToken(
            request.Dto.RefreshToken,
            newRefreshToken,
            refreshTokenExpireDate);

        var newAccessToken = await _tokenService.CreateTokenAsync(user, roles);

        await _userManager.UpdateAsync(user);

        var response = new RefreshTokenResponse
        {
            NewAccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            NewRefreshToken = newRefreshToken,
            RefreshTokenExpireDate = refreshTokenExpireDate
        };

        return Result<RefreshTokenResponse>
            .Success(response, 200);
    }
}
