using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class ConcurrencyException(
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "Concurrency.Error",
        ErrorMessages = errors
    };
}
