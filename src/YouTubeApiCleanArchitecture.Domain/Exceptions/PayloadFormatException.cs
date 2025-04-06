using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class PayloadFormatException(
    List<string> errors) : Exception, IBadRequest
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "PayloadFormat.Error",
        ErrorMessages = errors
    };
}