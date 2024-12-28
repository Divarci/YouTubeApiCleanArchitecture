using YouTubeApiCleanArchitecture.Application.Abstraction.Caching;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetCustomer;
public record GetCustomerQuery(
    Guid CustomerId) : IQuery<CustomerResponse>, ICachedQuery
{
    public string CacheKey => $"customer-{CustomerId}";

    public TimeSpan? Expiration => null;
}
