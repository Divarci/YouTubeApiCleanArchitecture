using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;

namespace YouTubeApiCleanArchitecture.DomainTests.Data.Invoices;
public static class InvoiceData
{

    public static CreateInvoiceItemDto CreateInvoiceItemDto
        => new()
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5
        };

    public static CreateInvoiceDto CreateInvoiceDto
        => new()
        {
            CustomerId = Guid.NewGuid(),
            PoNumber = "Test PO",
            PurchasedProducts = [CreateInvoiceItemDto]
        };

    public static async Task<Invoice> CreateInvoice(IUnitOfWork unitOfWork)
        => await Invoice.Create(CreateInvoiceDto, unitOfWork);
}
