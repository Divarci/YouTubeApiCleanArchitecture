using YouTubeApiCleanArchitecture.Application.Abstraction.Caching;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetProduct;
public record GetProductQuery(
    Guid ProductId) : IQuery<ProductResponse>, ICachedQuery
{
    public string CacheKey => $"product - {ProductId}";

    public TimeSpan? Expiration => null;
}
