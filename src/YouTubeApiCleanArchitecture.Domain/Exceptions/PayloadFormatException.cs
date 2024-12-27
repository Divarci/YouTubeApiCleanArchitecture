using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class PayloadFormatException(
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "PayloadFormat.Error",
        ErrorMessages = errors
    };
}