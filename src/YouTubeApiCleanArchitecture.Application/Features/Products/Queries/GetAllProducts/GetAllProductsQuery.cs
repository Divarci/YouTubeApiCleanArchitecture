using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
public record GetAllProductsQuery : IQuery<ProductResponseCollection>;
