using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace YouTubeApiCleanArchitecture.API.OpenApi;

public sealed class ConfigureSwaggerOptions(
    IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;

    public void Configure(
        string? name,
        SwaggerGenOptions options)
        => Configure(options);
    

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
    }

    public static OpenApiInfo CreateVersionInfo(
        ApiVersionDescription description)
    {
        var apiInfo = new OpenApiInfo
        {
            Title = $"YouTube.APi v{description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
            apiInfo.Description += "This API version has been deprecated";

        return apiInfo;
    }
}
