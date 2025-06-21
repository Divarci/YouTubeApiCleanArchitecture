using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.DomainTests.Data.Products;
public static class ProductData
{
    public static CreateProductDto CreateDto
        => new()
        {
            Description = "Test Product",
            UnitPrice = 5
        };

    public static Product CreateProduct()
        => Product.Create(CreateDto);
}
