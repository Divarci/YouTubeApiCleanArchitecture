using Microsoft.AspNetCore.Identity;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RevokeUser;
internal sealed class RevokeUserCommandHandler(
    UserManager<AppUser> userManager) : ICommandHandler<RevokeUserCommand, NoContentDto>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<Result<NoContentDto>> Handle(
        RevokeUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByEmailAsync(request.Dto.Email);

        if (user is null)
            return Result<NoContentDto>
                .Failed(400, new Error
                {
                    ErrorCode = "RevokeUser.Error",
                    ErrorMessages = ["User not exist"]
                });

        user.RevokeUser();

        await _userManager.UpdateAsync(user);

        return Result<NoContentDto>
            .Success(204);
    }
}
