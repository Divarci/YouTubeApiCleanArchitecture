using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.Behaviours;
public class LoggingBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : ILoggable
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;

        try
        {
            _logger.LogInformation("Executing request {RequestName}", requestName);

            var result = await next();

            if (result.IsNotSuccessfull)
            {
                using (LogContext.PushProperty("Error", result.Errors!.ErrorMessages, true))
                {
                    _logger.LogError("Request {RequestName} processed with error", requestName);
                }
            }
            else
            {
                _logger.LogInformation("Request {RequestName} processed successfully", requestName);
            }

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Request {RequestName} processing failed", requestName);
            throw;
        }
    }
}
