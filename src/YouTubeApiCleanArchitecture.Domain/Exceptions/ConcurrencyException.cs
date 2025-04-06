using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class ConcurrencyException(
    List<string> errors) : Exception, IBadRequest
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "Concurrency.Error",
        ErrorMessages = errors
    };
}
