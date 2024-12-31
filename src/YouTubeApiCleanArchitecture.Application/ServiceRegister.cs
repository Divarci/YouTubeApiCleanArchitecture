using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YouTubeApiCleanArchitecture.Application.Abstraction.Behaviours;

namespace YouTubeApiCleanArchitecture.Application;
public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        AddServicesToDiContainer(services);

        return services;
    }
    private static IServiceCollection AddServicesToDiContainer(
      this IServiceCollection services)
    {
        var applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(applicationAssembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(applicationAssembly);

            config.AddOpenBehavior(typeof(LoggingBehavior<,>));

            config.AddOpenBehavior(typeof(CachingBehaviour<,>));
        });

        return services;
    }  
}
