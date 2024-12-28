using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using YouTubeApiCleanArchitecture.API.Extensions;
using YouTubeApiCleanArchitecture.API.Filters;
using YouTubeApiCleanArchitecture.Application;
using YouTubeApiCleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ValidationFilterAttribute());
});

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestContextLogging();
app.UseSerilogRequestLogging(); 

app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("health-check", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
