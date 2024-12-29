using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using YouTubeApiCleanArchitecture.Application.Abstraction.Caching;
using YouTubeApiCleanArchitecture.Application.Abstraction.Emailing;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Infrastructure.Outbox;
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

        AddHealthChecks(services, config);

        AddApiVersioning(services);

        AddBackgroundJobs(services, config);

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

        services.AddScoped<IEmailService, EmailService>();

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

    private static IServiceCollection AddHealthChecks(
      this IServiceCollection services,
      IConfiguration config)
    {
        services.AddHealthChecks()
            .AddSqlServer(config.GetConnectionString("Database")!)
            .AddRedis(config.GetConnectionString("Cache")!);

        return services;
    }

    private static IServiceCollection AddApiVersioning(
     this IServiceCollection services)
    {
        services
            .AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1);
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'V";
                opt.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<OutboxOptions>(config.GetSection("Outbox"));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobsSetup>();

        return services;
    }

}
