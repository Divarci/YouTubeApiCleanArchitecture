using AutoMapper;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.RegisterUser;
public class RegisterUserResponse : IResult
{
    public Guid Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string Email { get; set; } = null!;    
}

public class RegisterUserMapper : Profile
{
    public RegisterUserMapper()
        => CreateMap<AppUser, RegisterUserResponse>();
}
