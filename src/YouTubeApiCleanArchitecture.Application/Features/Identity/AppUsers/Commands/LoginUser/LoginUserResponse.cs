using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.LoginUser;
public class LoginUserResponse : IResult
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
}
