using YouTubeApiCleanArchitecture.Domain.Entities.Shared;
using YouTubeApiCleanArchitecture.DomainTests.Data.Customers;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Customers;
public class Domain_Customer_IncreaseBalance
{
    [Fact]
    public void Customer_IncreaseBalance_ShouldUpdateCustomerBalance_Successfully()
    {
        // Arrange

        var newInvoiceAmount = new Money(125);

        var existingCustomer = CustomerData.CreateCustomer();

        var previousBalance = existingCustomer.Balance;

        // Act

        existingCustomer.IncreaseBalance(newInvoiceAmount);

        // Assert

        Assert.Multiple(() =>
        {
            Assert.NotNull(existingCustomer);           
            Assert.Equal(existingCustomer.Balance.Value,
                previousBalance.Value + newInvoiceAmount.Value);

        });

    }

}
