using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.ValueObjects;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems;
public sealed class InvoiceItem : BaseEntity
{
    private InvoiceItem() { }

    internal InvoiceItem(
        Guid id,
        Title description,
        Money sellPrice,
        Quantity quantity,
        Guid invoiceId) : base(id)
    {
        Description = description;
        SellPrice = sellPrice;
        Quantity = quantity;
        TotalPrice = new Money(SellPrice.Value * Quantity.Value);
        InvoiceId = invoiceId;
    }

    public Title Description { get; private set; } = null!;
    public Money SellPrice { get; private set; } = null!;
    public Quantity Quantity { get; private set; } = null!;

    public Money TotalPrice { get; private set; } = null!;

    public Guid InvoiceId { get; private set; }

    public Invoice Invoice { get; private set; } = null!;
}
