using YouTubeApiCleanArchitecture.Domain.Entities.Shared;
using YouTubeApiCleanArchitecture.DomainTests.Data.Customers;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Customers;
public class Domain_Customer_DecreaseBalance
{
    [Fact]
    public void Customer_DecreaseBalance_ShouldUpdateCustomerBalance_Successfully()
    {
        // Arrange

        var existingInvoiceAmount = new Money(125);

        var existingCustomer = CustomerData.CreateCustomer().AddBalance(250);

        var previousBalance = existingCustomer.Balance;

        // Act

        existingCustomer.DecreaseBalance(existingInvoiceAmount);

        // Assert

        Assert.Multiple(() =>
        {
            Assert.NotNull(existingCustomer);
            Assert.Equal(existingCustomer.Balance.Value,
                previousBalance.Value - existingInvoiceAmount.Value);

        });

    }
}
