namespace YouTubeApiCleanArchitecture.Domain.Abstraction;
public interface ILoggable
{
    public bool IsNotSuccessfull { get; set; }
    public Error? Errors { get; set; }


}
