using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
using YouTubeApiCleanArchitecture.DomainTests.Test.Shared;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Customers;
public class Domain_Customer_Create : BaseTest
{
    [Fact]
    public void Customer_Create_ShouldReturnCustomer_Successfully()
    {
        // Arrange

        var createDto = new CreateCustomerDto()
        {
            City = "London",
            Country = "United Kingdon",
            FirstLineAddress = "58 Rockstar street",
            Postcode = "MT157PO",
            Title = "Future Solutions Ltd."
        };

        // Act

        var customer = Customer.Create(createDto);

        // Assert

        Assert.Multiple(() =>
        {
            Assert.NotNull(customer);
            Assert.NotNull(customer.Address);
            Assert.Equal(createDto.Title, customer.Title.Value);
            Assert.Equal(createDto.Postcode, customer.Address.Postcode);
            Assert.Equal(createDto.FirstLineAddress, customer.Address.FirstLineAddress);
            Assert.Equal(createDto.Country, customer.Address.Country);
            Assert.Equal(createDto.City, customer.Address.City);
            Assert.Equal(createDto.SecondLineLineAddress, customer.Address.SecondLineAddress);

        });
    }

    [Fact]
    public void Customer_Create_ShouldRaiseDomainEvent_Successfully()
    {
        // Arrange

        var createDto = new CreateCustomerDto()
        {
            City = "London",
            Country = "United Kingdon",
            FirstLineAddress = "58 Rockstar street",
            Postcode = "MT157PO",
            Title = "Future Solutions Ltd."
        };

        // Act

        var customer = Customer.Create(createDto);

        // Assert

        var domainEvent = HandleRaiseDomainEvent<CustomerCreatedDomainEvent>(customer);

        Assert.Equal(customer.Id, domainEvent.CustomerId);
    }
        
    
}
