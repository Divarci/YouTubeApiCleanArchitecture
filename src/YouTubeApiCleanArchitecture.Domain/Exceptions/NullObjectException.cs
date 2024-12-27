using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class NullObjectException(
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "NullObject.Error",
        ErrorMessages = errors
    };
}
