using AutoMapper;
using Microsoft.AspNetCore.Identity;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Roles;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper)
    : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<RegisterUserResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await AppUser
            .Create(request.Dto, _userManager);

        var result = await _userManager.CreateAsync(user, request.Dto.Password);

        if (!result.Succeeded)
            return Result<RegisterUserResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "RegisterFailed.Error",
                    ErrorMessages = result.Errors.Select(x => x.Description).ToList()
                });

        var response = _mapper.Map<RegisterUserResponse>(user);

        return Result<RegisterUserResponse>
            .Success(response, 200);
    }
}
