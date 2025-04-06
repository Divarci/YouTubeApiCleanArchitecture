using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Abstraction;

public interface IINternalServerError
{
    public Error Errors { get; set; }
}