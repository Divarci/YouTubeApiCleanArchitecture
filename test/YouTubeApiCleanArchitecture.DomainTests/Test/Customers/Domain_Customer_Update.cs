using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.DomainTests.Data.Customers;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Customers;
public class Domain_Customer_Update
{
    [Fact]
    public void Customer_Update_ShouldUpdateCustomer_Successfully()
    {
        // Arrange

        var updateDto = CustomerData.UpdateDto;

        var existingCustomer = CustomerData.CreateCustomer();

        // Act

        existingCustomer.Update(updateDto);

        // Assert

        Assert.Multiple(() =>
        {
            Assert.NotNull(existingCustomer);
            Assert.NotNull(existingCustomer.Address);
            Assert.Equal(updateDto.Title, existingCustomer.Title.Value);
            Assert.Equal(updateDto.Postcode, existingCustomer.Address.Postcode);
            Assert.Equal(updateDto.FirstLineAddress, existingCustomer.Address.FirstLineAddress);
            Assert.Equal(updateDto.Country, existingCustomer.Address.Country);
            Assert.Equal(updateDto.City, existingCustomer.Address.City);
            Assert.Equal(updateDto.SecondLineLineAddress, existingCustomer.Address.SecondLineAddress);

        });

    }

}
