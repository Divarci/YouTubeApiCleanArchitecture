using Serilog.Context;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

namespace YouTubeApiCleanArchitecture.API.Middlewares;

public class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {

            var exceptionDetails = GetExceptionDetails(exception);

            using (LogContext.PushProperty("Error", exceptionDetails.Errors!.ErrorMessages, true))
            {
                _logger.LogError(exception, "Exception occured: {Message}", exception.Message);
            }

            context.Response.StatusCode = exceptionDetails.StatusCode;

            await context.Response.WriteAsJsonAsync(exceptionDetails);
        }
    }

    private static Result<NoContentDto> GetExceptionDetails(Exception exception) =>
        exception switch
        {
            IBadRequest badRequestException
                => Result<NoContentDto>.Failed(
                    StatusCodes.Status400BadRequest,
                    badRequestException.Errors),

            IINternalServerError InternalServerException
                => Result<NoContentDto>.Failed(
                    StatusCodes.Status500InternalServerError,
                    InternalServerException.Errors),

            _ => Result<NoContentDto>.Failed(
                    StatusCodes.Status500InternalServerError,
                    new Error
                    {
                        ErrorCode = "Internal Server Error",
                        ErrorMessages = ["Please seek an advise"]
                    })
        };
}
