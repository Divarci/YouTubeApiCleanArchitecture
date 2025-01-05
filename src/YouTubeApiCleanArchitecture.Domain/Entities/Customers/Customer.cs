using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.ValueObject;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers;
public sealed class Customer : BaseEntity
{
    private Customer(
        Guid customerId,
        Title title, 
        Address address, 
        Money balance) : base(customerId)
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

    public static Customer Create(CreateCustomerDto dto, Guid customerId)
    {
        var customer = new Customer(
            customerId,
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

    public void IncreaseBalance(Money invoiceAmount)
        => Balance = new Money(
            Balance.Value + invoiceAmount.Value);

    public void DecreaseBalance(decimal invoiceAmount)
        => Balance = new Money(
            Balance.Value - invoiceAmount);

    public void RemoveInvoice(Invoice invoice)
    {
        Invoices.Remove(invoice);

        RaiseDomainEvent(new InvoiceRemovedDomainEvent(
            invoice.Id,
            invoice.TotalBalance.Value));
    }

}
