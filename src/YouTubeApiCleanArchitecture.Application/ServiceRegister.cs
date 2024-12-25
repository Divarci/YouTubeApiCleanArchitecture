using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace YouTubeApiCleanArchitecture.Application;
public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
