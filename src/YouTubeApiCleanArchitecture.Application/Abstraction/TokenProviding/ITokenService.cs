using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.TokenProviding;
public interface ITokenService
{
    Task<JwtSecurityToken> CreateTokenAsync(
        AppUser user, 
        IList<string> roles);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);

    DateTime GetRefreshTokenExpireDate();
}
