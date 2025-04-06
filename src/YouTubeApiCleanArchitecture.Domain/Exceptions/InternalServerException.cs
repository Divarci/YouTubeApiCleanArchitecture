using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class InternalServerException(
    string errorCode,
    List<string> errors) : Exception, IINternalServerError
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = errorCode,
        ErrorMessages = errors
    };
}
