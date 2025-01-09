using FluentAssertions;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;
public class CustomerTests
{
    [Fact]
    public void Create_ShouldReturn_Success()
    {
        //Arrange

        Guid customerId = Guid.Parse("2DB02220-FE13-417F-9461-5AB2EF332004");

        //Act

        var customer = CustomerData
            .CreateQuickCustomer(customerId);

        var @event = customer.GetDomainEvents()[0] as CustomerCreatedDomainEvent;

        //Assert

        customer.Should()
            .NotBeNull();

        customer.Id.Should()
            .Be(customerId);

        customer.Title.Value.Should()
            .Be(CustomerData.TitleList[0]);

        customer.Address.FirstLineAddress.Should()
            .Be(CustomerData.FirstLineAddressList[1]);

        customer.Address.SecondLineAddress.Should()
            .BeNull();

        customer.Address.City.Should()
            .Be(CustomerData.CityList[2]);

        customer.Address.Postcode.Should()
            .Be(CustomerData.PostcodeList[0]);

        customer.Address.Country.Should()
            .Be(CustomerData.Country);

        customer.Invoices.Should()
            .BeNullOrEmpty();

        customer.Balance.Value.Should()
            .Be(0);

        customer.GetDomainEvents().Should()
            .HaveCount(1);

        @event.Should()
            .NotBeNull();

        @event.Should()
            .BeAssignableTo<CustomerCreatedDomainEvent>();

        @event!.CustomerId.Should()
            .Be(customerId);
    }

    [Fact]
    public void Update_ShouldReturn_Success()
    {
        //Arrange

        Guid customerId = Guid.Parse("2DB02220-FE13-417F-9461-5AB2EF332004");

        var updateDto = new UpdateCustomerDto
        {
            Title = CustomerData.TitleList[2],
            FirstLineAddress = CustomerData.FirstLineAddressList[2],
            SecondLineLineAddress = CustomerData.SecondLineAddressList[0],
            Postcode = CustomerData.PostcodeList[1],
            City = CustomerData.CityList[1],
            Country = CustomerData.Country
        };

        //Act

        var customer = CustomerData
            .CreateQuickCustomer(customerId);

        customer.Update(updateDto);

        //Assert

        customer.Should()
            .NotBeNull();

        customer.Id.Should()
            .Be(customerId);

        customer.Title.Value.Should()
            .Be(CustomerData.TitleList[2]);

        customer.Address.FirstLineAddress.Should()
            .Be(CustomerData.FirstLineAddressList[2]);

        customer.Address.SecondLineAddress.Should()
            .Be(CustomerData.SecondLineAddressList[0]);

        customer.Address.City.Should()
            .Be(CustomerData.CityList[1]);

        customer.Address.Postcode.Should()
            .Be(CustomerData.PostcodeList[1]);

        customer.Address.Country.Should()
            .Be(CustomerData.Country);
    }

    [Fact]
    public void IncreaseBalance_ShouldReturn_Success()
    {
        //Arrange
        Guid customerId = Guid.Parse("2DB02220-FE13-417F-9461-5AB2EF332004");

        var additionOne = new Money(1254.55m);
        var additionTwo = new Money(2546.95m);

        //Act

        var customer = CustomerData
            .CreateQuickCustomer(customerId);

        //Assert & Act

        customer.IncreaseBalance(additionOne);

        customer.Balance.Should()
            .Be(additionOne);

        customer.IncreaseBalance(additionTwo);

        customer.Balance.Should()
            .Be(new Money(additionOne.Value + additionTwo.Value));

    }

    [Fact]
    public void DecreaseBalance_ShouldReturn_Success()
    {
        //Arrange
        Guid customerId = Guid.Parse("2DB02220-FE13-417F-9461-5AB2EF332004");

        var initialBalance = new Money(4526.55m);

        var dedutionOne = 452.45m;

        var dedutionTwo = 25.25m;

        //Act

        var customer = CustomerData
            .CreateQuickCustomer(customerId);

        //Assert & Act

        customer.IncreaseBalance(initialBalance);

        customer.DecreaseBalance(dedutionOne);

        customer.Balance.Value.Should()
            .Be(initialBalance.Value - dedutionOne);

        customer.DecreaseBalance(dedutionTwo);

        customer.Balance.Value.Should()
            .Be(initialBalance.Value - dedutionOne - dedutionTwo);
    }
}
