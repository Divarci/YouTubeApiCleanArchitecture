using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class InternalServerException(
    string errorCode,
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = errorCode,
        ErrorMessages = errors
    };
}
