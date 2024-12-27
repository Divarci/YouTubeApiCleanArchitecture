using Serilog.Context;

namespace YouTubeApiCleanArchitecture.API.Middlewares;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CORRELATION_ID_HEADER_NAME = "X-Correlation-Id";
    private readonly RequestDelegate _next = next;

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        {
            return _next(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(CORRELATION_ID_HEADER_NAME, out var correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
