namespace YouTubeApiCleanArchitecture.Infrastructure.Services.TokenProviding;
public class TokenSettings
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int TokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInDates { get; set; }
}
