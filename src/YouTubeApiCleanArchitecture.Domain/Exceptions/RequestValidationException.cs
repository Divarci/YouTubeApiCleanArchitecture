using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class RequestValidationException(
    List<string> errors) : Exception, IBadRequest
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "Validation.Error",
        ErrorMessages = errors
    };
}
