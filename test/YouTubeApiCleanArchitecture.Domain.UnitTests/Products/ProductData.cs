using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Products;
public static class ProductData
{
    public static readonly Guid CementId = Guid.NewGuid();
    public static readonly Guid BrickId = Guid.NewGuid();
    public static readonly Guid GlueId = Guid.NewGuid();
    public static readonly Guid WoodId = Guid.NewGuid();

    public static readonly Dictionary<Guid, KeyValuePair<string, decimal>> ProductList =
        new()
        {
            {CementId, new("Cement", 2.25m )},
            {BrickId, new ("Brick",3.15m)},
            {GlueId , new("Glue", 4.35m)},
            {WoodId , new("Wood", 5.25m)}
        };

    public static readonly List<Product> Catalogue = LoadProductData();

    private static List<Product> LoadProductData()
    {
        var catalogue = new List<Product>();

        foreach (var product in ProductList)
        {
            var tempProduct = Product
                .Create(
                    new CreateProductDto
                    {
                        Description = product.Value.Key,
                        UnitPrice = product.Value.Value
                    },
                    product.Key);

            catalogue.Add(tempProduct);
        }

        return catalogue;
    }

    public static Product CreateProduct(        
        Guid productId)
    {
        var prouct = Product.Create(
            new CreateProductDto
            {
                Description = ProductList[productId].Key,
                UnitPrice = ProductList[productId].Value
            },
            productId);

        return prouct;
    }
}
