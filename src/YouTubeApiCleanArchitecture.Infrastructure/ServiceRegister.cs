using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTubeApiCleanArchitecture.Application.Abstraction.Caching;
using YouTubeApiCleanArchitecture.Application.Abstraction.Emailing;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Infrastructure.Repositories;
using YouTubeApiCleanArchitecture.Infrastructure.Services.Caching;
using YouTubeApiCleanArchitecture.Infrastructure.Services.Emailing;
using YouTubeApiCleanArchitecture.Infrastructure.UnitOfWorks;

namespace YouTubeApiCleanArchitecture.Infrastructure;
public static class ServiceRegister
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        AddDbConnection(services, config);

        AddServicesToDiContainer(services);

        AddCaching(services, config);

        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("Database"));
        });
        return services;
    }

    private static IServiceCollection AddServicesToDiContainer(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IEmailService,EmailService>();

        return services;
    }

    private static IServiceCollection AddCaching(
       this IServiceCollection services,
       IConfiguration config)
    {
        services.AddStackExchangeRedisCache(opt
            => opt.Configuration = config.GetConnectionString("Cache"));

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }

}
