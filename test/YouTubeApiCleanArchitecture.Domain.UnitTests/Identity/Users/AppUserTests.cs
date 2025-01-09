using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Identity.Users;
public class AppUserTests
{
    private readonly UserManager<AppUser> _userManager;

    public AppUserTests()
    {
        var userStore = Substitute.For<IUserStore<AppUser>>();

        _userManager = Substitute.For<UserManager<AppUser>>(
            userStore, null!, null!, null!, null!, null!, null!, null!, null!);

        var userList = new List<AppUser>
        {
            AppUserData.CreateExistingValidUserAsync(_userManager).Result,
            AppUserData.CreateExistingValidAdminAsync(_userManager).Result
        };

        foreach (var user in userList)
        {
            _userManager
                .FindByEmailAsync(user.Email!)
                .Returns(user);
        }
    }

    [Fact]
    public async Task Create_ShouldReturn_Success_WhenUserNotExist()
    {
        // Act

        var user = await AppUserData
            .CreateNewValidUserAsync(_userManager);

        var @event = user.GetDomainEvents()[0] as UserRegisteredDomainEvent;

        // Assert
        user.Should()
            .NotBeNull();

        user.Fullname.Should()
            .Be(AppUserData.New_User_Fullname);

        user.Email.Should()
            .Be(AppUserData.New_User_Email);

        user.UserName.Should()
            .Be(AppUserData.New_User_Email);

        user.GetDomainEvents().Should()
            .HaveCount(1);

        @event.Should()
            .NotBeNull();

        @event!.UserId.Should()
            .Be(user.Id);

        @event.AdminKey.Should()
            .BeNull();
    }

    [Fact]
    public async Task Create_ShouldThrowException_WhenUserAlreadyExists()
    {
        // Act

        Func<Task> act = async () => await AppUserData
            .CreateExistingValidUserAsync(_userManager);

        // Assert

        await act.Should()
            .ThrowAsync<UserAlreadyExistException>();
    }

    [Fact]
    public async Task AddRefreshTokenInfo_ShouldUpdate_RefreshTokenAndExpireDate()
    {
        // Arrange

        var user = await AppUserData
            .CreateNewValidUserAsync(_userManager);

        var refreshToken = "test-refresh-token";

        var expireDate = DateTime.Now.AddDays(7);

        // Act

        user.AddRefreshTokenInfo(refreshToken, expireDate);

        // Assert

        user.RefreshToken.Should()
            .Be(refreshToken);

        user.RefreshTokenExpireDate.Should()
            .Be(expireDate);
    }

    [Fact]
    public async Task UpdateRefreshToken_ShouldUpdateToNewToken_WhenValid()
    {
        // Arrange

        var user = await AppUserData
            .CreateNewValidUserAsync(_userManager);

        var refreshToken = "test-refresh-token";

        var expireDate = DateTime.Now.AddDays(7);

        user.AddRefreshTokenInfo(refreshToken, expireDate);

        var newRefreshToken = "new-refresh-token";

        var newExpireDate = DateTime.Now.AddDays(14);

        // Act

        user.UpdateRefreshToken(refreshToken, newRefreshToken, newExpireDate);

        // Assert

        user.RefreshToken.Should()
            .Be(newRefreshToken).And.NotBe(refreshToken);

        user.RefreshTokenExpireDate.Should()
            .Be(newExpireDate).And.NotBe(expireDate);
    }

    [Fact]
    public async Task UpdateRefreshToken_ShouldThrowException_WhenTokenIsExpired()
    {
        // Act        

        Func<Task> act = async () =>
        {
            var user = await AppUserData
                .CreateNewValidUserAsync(_userManager);

            var refreshToken = "test-refresh-token";

            var expireDate = DateTime.Now.AddDays(-7);

            user.AddRefreshTokenInfo(refreshToken, expireDate);

            var newRefreshToken = "new-refresh-token";

            var newExpireDate = DateTime.Now.AddDays(14);

            user.UpdateRefreshToken(refreshToken, newRefreshToken, newExpireDate);
        };

        // Assert

        await act.Should()
            .ThrowAsync<InvalidTokenException>();
    }

    [Fact]
    public async Task UpdateRefreshToken_ShouldThrowException_WhenTokenIsInvalid()
    {
        // Arrange

        Func<Task> act = async () =>
        {
            var user = await AppUserData
                .CreateNewValidUserAsync(_userManager);

            var refreshToken = "test-refresh-token";

            var expireDate = DateTime.Now.AddDays(7);

            user.AddRefreshTokenInfo(refreshToken, expireDate);

            var newRefreshToken = "new-refresh-token";

            var newExpireDate = DateTime.Now.AddDays(14);

            user.UpdateRefreshToken("wrong token info", newRefreshToken, newExpireDate);
        };

        // Assert

        await act.Should()
            .ThrowAsync<InvalidTokenException>();
    }

    [Fact]
    public async Task RevokeUser_ShouldClearRefreshTokenAndExpireDate()
    {
        // Arrange

        var user = await AppUserData
            .CreateNewValidUserAsync(_userManager);

        var refreshToken = "test-refresh-token";

        var expireDate = DateTime.Now.AddDays(7);

        user.AddRefreshTokenInfo(refreshToken, expireDate);

        // Act

        user.RevokeUser();

        // Assert

        user.RefreshToken.Should()
            .BeNull();

        user.RefreshTokenExpireDate.Should()
            .BeNull();
    }
}
