using Microsoft.AspNetCore.Identity;
using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.Events;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;
public class AppUser : IdentityUser<Guid>, IDomainEventRaiser
{
    private readonly List<IDomainEvent> _domainEvents = [];

    private AppUser(
        Guid id,
        string fullname,
        string email,
        string userName) : base(userName)
    {
        Id = id;
        Fullname = fullname;
        Email = email;
        SecurityStamp = Guid.NewGuid().ToString();
    }

    private AppUser() { }

    public string Fullname { get; private set; } = null!;
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpireDate { get; private set; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
       => _domainEvents.ToList();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);


    public static async Task<AppUser> Create(
        RegisterUserDto dto, 
        UserManager<AppUser> userManager)
    {
        var isUserExist = await userManager.FindByEmailAsync(dto.Email) is not null;

        if (isUserExist)
            throw new UserAlreadyExistException(
                ["User is already exist"]);

        var user = new AppUser(Guid.NewGuid(), dto.Fullname, dto.Email, dto.Email);

        user.RaiseDomainEvent(
            new UserRegisteredDomainEvent(user.Id, dto.AdminKey));

        return user;
    }   

    public void AddRefreshTokenInfo(
        string refreshToken,
        DateTime refreshTokenExpireDate)
    {     
        RefreshToken = refreshToken;
        RefreshTokenExpireDate = refreshTokenExpireDate;
    }  

    public void UpdateRefreshToken(
        string existingRefreshToken,
        string newRefreshToken,
        DateTime newRefreshTokenExpireDate)
    {
        if (!string.IsNullOrEmpty(RefreshToken) && !RefreshToken.Equals(existingRefreshToken))
            throw new InvalidTokenException(
                ["Invalid Refresh token."]);

        if (RefreshTokenExpireDate.HasValue && RefreshTokenExpireDate < DateTime.Now)
            throw new InvalidTokenException(
                ["Refresh token has expired. Please LogIn again."]);

        RefreshToken = newRefreshToken;
        RefreshTokenExpireDate = newRefreshTokenExpireDate;
    }

    public void RevokeUser()
    {
        RefreshToken = null;
        RefreshTokenExpireDate = null;
    }    
}
