using FluentAssertions;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Products;
public class ProductTests
{
    [Fact]
    public void Create_ShouldReturn_Success()
    {        
        //Act

        var productCement = ProductData.CreateProduct(ProductData.CementId);

        //Assert

        productCement.Should()
           .NotBeNull();

        productCement.Id.Should()
            .Be(ProductData.CementId);

        productCement.Description.Value.Should()
            .Be(ProductData.ProductList[ProductData.CementId].Key);

        productCement.UnitPrice.Value.Should()
            .Be(ProductData.ProductList[ProductData.CementId].Value);
    }


    [Fact]
    public void Update_ShouldReturn_Success()
    {
        //Arrange

        var updateDto = new UpdateProductDto
        {
            Description = "Bucket",
            UnitPrice = 12
        };

        //Act

        var productCement = ProductData.CreateProduct(ProductData.CementId);

        productCement.Update(updateDto);

        //Assert

        productCement.Should()
           .NotBeNull();

        productCement.Id.Should()
            .Be(ProductData.CementId);

        productCement.Description.Value.Should()
            .Be(updateDto.Description);

        productCement.UnitPrice.Value.Should()
            .Be(updateDto.UnitPrice);
    }
}
