using System.ComponentModel.DataAnnotations;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
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
            _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

            var exceptionDetails = GetExceptionDetails(exception);

            context.Response.StatusCode = exceptionDetails.StatusCode;

            await context.Response.WriteAsJsonAsync(exceptionDetails);
        }
    }

    private static Result<NoContentDto> GetExceptionDetails(Exception exception) =>
        exception switch
        {
            RequestValidationException validationException
                => Result<NoContentDto>.Failed(
                        StatusCodes.Status400BadRequest,
                        validationException.Errors),

            ConcurrencyException concurrencyException
                 => Result<NoContentDto>.Failed(
                        StatusCodes.Status400BadRequest,
                        concurrencyException.Errors),

            NullObjectException nullObjectException
                 => Result<NoContentDto>.Failed(
                        StatusCodes.Status400BadRequest,
                        nullObjectException.Errors),

            BadRequestException badRequestException
                => Result<NoContentDto>.Failed(
                        StatusCodes.Status400BadRequest,
                        badRequestException.Errors),

            PayloadFormatException payloadFormatException
               => Result<NoContentDto>.Failed(
                       StatusCodes.Status400BadRequest,
                       payloadFormatException.Errors),

            _ => Result<NoContentDto>.Failed(
                    StatusCodes.Status500InternalServerError,
                    new Error
                    {
                        ErrorCode = "Internal Server Error",
                        ErrorMessages = ["Please see an advise"]
                    })
        };
}
