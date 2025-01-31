using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using YouTubeApiCleanArchitecture.API.FunctionalTests.Infrastructure;
using YouTubeApiCleanArchitecture.Application.Features.Identity.AppUsers.Commands.LoginUser;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;

namespace YouTubeApiCleanArchitecture.API.FunctionalTests.Identity;
public class AuthTests : BaseFunctionalTest
{
    public AuthTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Register_ShouldReturn_Success()
    {
        //arrange

        var request = new RegisterUserDto
        {
            Email = "test2@test.com",
            Password = "Password12**",
            ConfirmPassword = "Password12**",
            Fullname = "Test2 Person"
        };

        //act

        var response = await HttpClient.PostAsJsonAsync(
            "api/v1/auth/register",
            request);

        //assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_ShouldReturn_Success()
    {
        //arrange

        var request = new LoginUserDto
        {
            Email = "test2@test.com",
            Password = "Password12**"           
        };

        //act

        var response = await HttpClient.PostAsJsonAsync(
            "api/v1/auth/login",
            request);

        var content = await response.Content
            .ReadFromJsonAsync<Result<LoginUserResponse>>();

        //assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        content.Data.AccessToken.Should().NotBeNull();
    }
}
