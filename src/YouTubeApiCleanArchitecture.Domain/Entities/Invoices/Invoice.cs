﻿using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.ValueObjects;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.ValueObjects;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
public sealed class Invoice : BaseEntity
{
    private Invoice() { }

    private Invoice(
        Guid invoiceId,
        PoNumber poNumber,
        Guid customerId,
        ICollection<InvoiceItem> purchasedProducts,
        Money totalBalance) : base(invoiceId)
    {
        PoNumber = poNumber;
        CustomerId = customerId;
        PurchasedProducts = purchasedProducts;
        TotalBalance = totalBalance;
    }

    public PoNumber PoNumber { get; private set; } = null!;

    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;

    public ICollection<InvoiceItem> PurchasedProducts { get; private set; } = null!;

    public Money TotalBalance { get; private set; } = null!;


    public static async Task<Invoice> Create(
        CreateInvoiceDto request,
        IUnitOfWork unitOfWork)
    {
        if (request.PurchasedProducts is null || request.PurchasedProducts.Count == 0)
            throw new InvalidOperationException("Empty Invoice can not be created");

        var invoiceId = Guid.NewGuid();
        ICollection<InvoiceItem> purchasedProducts = [];

        foreach (var purchasedProduct in request.PurchasedProducts)
        {
            var product = await unitOfWork
                .Repository<Product>()
                .GetByIdAsync(purchasedProduct.ProductId) ??
                throw new ArgumentNullException($"Product with id: {purchasedProduct.ProductId} not found");

            var invoiceItem = new InvoiceItem(
                Guid.NewGuid(),
                new Money(product.UnitPrice.Value),
                new Quantity(purchasedProduct.Quantity),
                invoiceId);

            purchasedProducts.Add(invoiceItem);
        }

        var totalBalance = purchasedProducts
            .Sum(x => x.TotalPrice.Value);

        var invoice = new Invoice(
           invoiceId,
           new PoNumber(request.PoNumber),
           request.CustomerId,
           purchasedProducts,
           new Money(totalBalance));

        invoice.RaiseDomainEvent(
            new InvoiceCreatedDomainEvent(invoiceId));

        return invoice;
    }

    public void Update(UpdateInvoiceDto request)
    {
        PoNumber = new PoNumber(request.PoNumber);
    }
}