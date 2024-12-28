using Microsoft.Extensions.Caching.Distributed;
using System.Buffers;
using System.Text.Json;
using YouTubeApiCleanArchitecture.Application.Abstraction.Caching;

namespace YouTubeApiCleanArchitecture.Infrastructure.Services.Caching;
internal sealed class CacheService(
    IDistributedCache cache) : ICacheService
{
    private readonly IDistributedCache _cache = cache;

    public async Task<T?> GetAsync<T>(
        string key,
        CancellationToken cancellationToken = default)
    {
        byte[]? bytes = await _cache.GetAsync(key, cancellationToken);

        return bytes is null ? default : Deserialize<T>(bytes);
    }

    public Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(key, cancellationToken);

    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        byte[] bytes = Serialize<T>(value);

        return _cache.SetAsync(key, bytes, CacheOptions.Create(expiration) , cancellationToken);
    }

    private static T Deserialize<T>(byte[] bytes)
        => JsonSerializer.Deserialize<T>(bytes)!;

    private static byte[] Serialize<T>(T value)
    {
        var buffer = new ArrayBufferWriter<byte>();

        using var writer = new Utf8JsonWriter(buffer);

        JsonSerializer.Serialize(writer, value);

        return buffer.WrittenSpan.ToArray();
    }
}

public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration
        => new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration)
        => expiration is not null
            ? new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expiration }
            : DefaultExpiration;

}
