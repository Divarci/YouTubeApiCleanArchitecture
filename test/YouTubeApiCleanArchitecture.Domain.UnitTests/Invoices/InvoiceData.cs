using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Products;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Invoices;
public static class InvoiceData
{
    public const string PoNumber = "PO:123456";

    public static readonly Guid InvoiceId = Guid.NewGuid();

    public static readonly Customer Customer = CustomerData
        .CreateQuickCustomer(Guid.NewGuid());

    public static readonly List<CreateInvoiceItemDto> ValidPurchasedProducts =
        [
            new() { ProductId = ProductData.GlueId, Quantity = 5 },
            new() { ProductId = ProductData.CementId, Quantity = 15 },
        ];

    public static readonly List<CreateInvoiceItemDto> PurchasedProductsWithInvalidId =
        [
            new() { ProductId = Guid.Empty, Quantity = 5 },
            new() { ProductId = ProductData.CementId, Quantity = 15 },
        ];

    public static readonly List<CreateInvoiceItemDto> PurchasedProductsWithInvalidQuantity =
       [
           new() { ProductId = ProductData.GlueId, Quantity = -5 },
            new() { ProductId = ProductData.CementId, Quantity = 15 },
        ];

    public static async Task<Invoice> CreateInvoice(
        Guid customerId, 
        Guid invoiceId,
        ICollection<CreateInvoiceItemDto> purchasedProducts,
        IUnitOfWork unitOfWork)
    {
        var invoice = await Invoice.Create(
            new CreateInvoiceDto
            {
                CustomerId = customerId,
                PoNumber = PoNumber,
                PurchasedProducts = purchasedProducts
            },
            invoiceId,
            unitOfWork);

        return invoice;
    }
}

