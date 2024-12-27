using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.ValueObjects;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.ValueObjects;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

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
        CreateInvoiceDto dto,
        IUnitOfWork unitOfWork)
    {
        if(dto.CustomerId == Guid.Empty)
            throw new BadRequestException(
                ["Customer Id is required"]);

        if (dto.PurchasedProducts is null || dto.PurchasedProducts.Count == 0)
            throw new BadRequestException(
                ["Empty Invoice can not be created"]);

        if( dto.PurchasedProducts.Any(x=>x.ProductId == Guid.Empty))
            throw new BadRequestException(
                ["Product Id(s) is/are missing in your purchased product list"]);

        if (dto.PurchasedProducts.Any(x => x.Quantity <= 0))
            throw new BadRequestException(
                ["Product Quantity must be set and must be positive number in your purchased product list"]);

        var invoiceId = Guid.NewGuid();
        ICollection<InvoiceItem> purchasedProducts = [];

        foreach (var purchasedProduct in dto.PurchasedProducts)
        {
            var product = await unitOfWork
                .Repository<Product>()
                .GetByIdAsync(purchasedProduct.ProductId) ??
                throw new NullObjectException(
                    [$"Product with id: {purchasedProduct.ProductId} not found"]);

            var invoiceItem = new InvoiceItem(
                Guid.NewGuid(),
                product.Description,
                product.UnitPrice,
                new Quantity(purchasedProduct.Quantity),
                invoiceId);

            purchasedProducts.Add(invoiceItem);
        }

        var totalBalance = purchasedProducts
            .Sum(x => x.TotalPrice.Value);

        var invoice = new Invoice(
           invoiceId,
           new PoNumber(dto.PoNumber),
           dto.CustomerId,
           purchasedProducts,
           new Money(totalBalance));

        invoice.RaiseDomainEvent(
            new InvoiceCreatedDomainEvent(invoiceId));

        return invoice;
    }

    public void Update(UpdateInvoiceDto dto)
    {
        PoNumber = new PoNumber(dto.PoNumber);
    }
}
