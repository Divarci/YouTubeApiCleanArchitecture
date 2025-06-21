using YouTubeApiCleanArchitecture.DomainTests.Data.Customers;
using YouTubeApiCleanArchitecture.DomainTests.Test.Shared;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Customers;
public class Domain_Customer_RemoveInvoice: BaseTest
{
    [Fact]
    public void Customer_Create_ShouldReturnCustomer_Successfully()
    {
        // Act

        var customer = CustomerData.CreateCustomer();

        // Assert

        Assert.Multiple(() =>
        {
            Assert.NotNull(customer);
            Assert.NotNull(customer.Address);
            Assert.Equal(CustomerData.CreateDto.Title, customer.Title.Value);
            Assert.Equal(CustomerData.CreateDto.Postcode, customer.Address.Postcode);
            Assert.Equal(CustomerData.CreateDto.FirstLineAddress, customer.Address.FirstLineAddress);
            Assert.Equal(CustomerData.CreateDto.Country, customer.Address.Country);
            Assert.Equal(CustomerData.CreateDto.City, customer.Address.City);
            Assert.Equal(CustomerData.CreateDto.SecondLineLineAddress, customer.Address.SecondLineAddress);

        });
    }
}
