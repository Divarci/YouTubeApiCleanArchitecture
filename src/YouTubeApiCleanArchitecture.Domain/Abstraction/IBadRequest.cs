using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Domain.Abstraction;

public interface IBadRequest
{
    public Error Errors { get; set; }
}