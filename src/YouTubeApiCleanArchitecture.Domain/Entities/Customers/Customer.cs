using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.ValueObject;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers;
public sealed class Customer : BaseEntity
{
    private Customer(
        Title title, 
        Address address, 
        Money balance)
    {
        Title = title;
        Address = address;
        Balance = balance;
    }

    private Customer() { }

    public Title Title { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public Money Balance { get; private set; } = null!;


    public ICollection<Invoice> Invoices { get; private set; } = null!;

    public static Customer Create(CreateCustomerDto dto)
    {
        var customer = new Customer(
            new Title(dto.Title),
            new Address(
                dto.FirstLineAddress,
                dto.SecondLineLineAddress,
                dto.Postcode,
                dto.City,
                dto.Country),
            new Money(0));

        customer.RaiseDomainEvent(
            new CustomerCreatedDomainEvent(customer.Id));

        return customer;
    }

    public void Update(UpdateCustomerDto dto)
    {
        Title = new Title(dto.Title);
        Address = new Address(
                dto.FirstLineAddress,
                dto.SecondLineLineAddress,
                dto.Postcode,
                dto.City,
                dto.Country);
    }

    public void UpdateBalance(Money invoiceAmount)
        => Balance = new Money(
            Balance.Value + invoiceAmount.Value);
    
}
