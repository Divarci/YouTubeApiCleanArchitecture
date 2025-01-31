using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;
using YouTubeApiCleanArchitecture.Infrastructure;

namespace YouTubeApiCleanArchitecture.API.FunctionalTests.Infrastructure;
public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient;
    private readonly IServiceScope _scope;
    protected readonly AppDbContext DbContext;


    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();

        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

        DbContext.Database.Migrate();
    }

    protected async Task<string> GetAccessToken()
    {
        HttpResponseMessage loginResponse = await HttpClient.PostAsJsonAsync(
            "api/v1/auth/login",
            new LoginUserDto
            {
                Email = "test@test.com",
                Password = "Password12**"
            });

        var accessTokenResponse = await loginResponse.Content
            .ReadFromJsonAsync<AccessTokenResponse>();

        return accessTokenResponse!.AccessToken;
    }
}
