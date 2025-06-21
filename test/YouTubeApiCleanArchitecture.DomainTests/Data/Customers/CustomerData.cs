using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.DomainTests.Data.Customers;
public static class CustomerData
{
    public static CreateCustomerDto CreateDto 
        => new()
        {
            City = "London",
            Country = "United Kingdon",
            FirstLineAddress = "58 Rockstar street",
            Postcode = "MT157PO",
            Title = "Future Solutions Ltd."
        };

    public static UpdateCustomerDto UpdateDto
        => new()
        {
            City = "Manchester",
            Country = "United Kingdon",
            FirstLineAddress = "98 Goldstar street",
            Postcode = "KL458PL",
            Title = "Renewing Solutions Ltd."
        };

    public static Customer CreateCustomer() 
        => Customer.Create(CreateDto);

    public static Customer AddBalance(this Customer customer, decimal Balance)
    {
        customer.IncreaseBalance(new Money(Balance));
        return customer;
    }

    public static Customer AddInvoice(this Customer customer,Invoice invoice)
    {
        customer.Invoices.Add(invoice);
        return customer;
    }
}
