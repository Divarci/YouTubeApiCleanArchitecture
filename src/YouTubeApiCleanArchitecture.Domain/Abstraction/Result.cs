namespace YouTubeApiCleanArchitecture.Domain.Abstraction;
public class Result
{
    internal Result(
        int statusCode,
        bool isNotSuccessfull,
        Dictionary<string, string>? errors)
    {
        StatusCode = statusCode;
        IsNotSuccessfull = isNotSuccessfull;
        Errors = errors;
    }

    public int StatusCode { get; private set; }

    public bool IsNotSuccessfull { get; private set; }

    public Dictionary<string, string>? Errors { get; private set; }


    public static Result Failed(
        int statusCode,
        string errorCode,
        string error)
        => new(
            statusCode: statusCode,
            isNotSuccessfull: false,
            errors: new Dictionary<string, string>
             {
                 {errorCode, error},
             });

    public Result Failed(
        int statusCode,
        string errorCode,
        Dictionary<string, string> errors)
        => new(
            statusCode: statusCode,
            isNotSuccessfull: false,
            errors: errors);

    public Result Success(int statusCode)
        => new(
            statusCode: statusCode,
            isNotSuccessfull: false,
            errors: null);

}

public class Result<TEntity> : Result
    where TEntity : BaseEntity
{
    internal Result(
        TEntity data,
        int statusCode,
        bool isNotSuccessfull) : base(statusCode, isNotSuccessfull, null)
    {
        Data = data;
    }

    public TEntity Data { get; private set; }

    public Result<TEntity> Success(
       TEntity data,
       int statusCode)
       => new(
           data: data,
           statusCode: statusCode, 
           isNotSuccessfull: false);
}

