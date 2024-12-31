using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using YouTubeApiCleanArchitecture.Application.Abstraction.TokenProviding;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

namespace YouTubeApiCleanArchitecture.Infrastructure.Services.TokenProviding;
public class TokenService(
    UserManager<AppUser> userManager,
    IOptions<TokenSettings> tokenOptions) : ITokenService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly TokenSettings _tokenOptions = tokenOptions.Value;

    public async Task<JwtSecurityToken> CreateTokenAsync(
        AppUser user,
        IList<string> roles)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email,user.Email!),
        };

        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_tokenOptions.Secret));

        var token = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            expires: DateTime.Now.AddMinutes(_tokenOptions.TokenValidityInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256));

        await _userManager.AddClaimsAsync(user, claims);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenOptions.Secret)),
            ValidateLifetime = false,
        };

        JwtSecurityTokenHandler tokenHandler = new();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecuritytoken ||
           !jwtSecuritytoken.Header.Alg.Equals(
               SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidTokenException(
                ["Provided Access Token is invalid"]);

        return principal;
    }

    public DateTime GetRefreshTokenExpireDate()
        => DateTime.Now.AddDays(_tokenOptions.RefreshTokenValidityInDates);
}
