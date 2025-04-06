namespace YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
public interface ILoggable
{
    public bool IsNotSuccessfull { get; set; }
    public Error? Errors { get; set; }


}
