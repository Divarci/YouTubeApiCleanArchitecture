using MediatR;
using Microsoft.Extensions.Logging;
using YouTubeApiCleanArchitecture.Application.Abstraction.Caching;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.Behaviours;
public class CachingBehaviour<TRequest, TResponse>(
    ICacheService cacheService,
    ILogger<CachingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : ILoggable
{
    private readonly ICacheService _cacheService = cacheService;
    private readonly ILogger<CachingBehaviour<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var cachedResult = await _cacheService.GetAsync<TResponse>(
            request.CacheKey,
            cancellationToken);

        string requestName = typeof(TRequest).Name;

        if (cachedResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", requestName);

            return cachedResult;
        }

        _logger.LogInformation("Cache miss for {Query}", requestName);

        var result = await next();

        if (!result.IsNotSuccessfull)
        {
            await _cacheService.SetAsync(
                request.CacheKey,
                result,
                request.Expiration,
                cancellationToken);
        }

        return result;
    }
}
