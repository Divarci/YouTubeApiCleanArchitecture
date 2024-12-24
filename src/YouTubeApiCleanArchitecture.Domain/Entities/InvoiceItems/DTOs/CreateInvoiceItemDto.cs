namespace YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.DTOs;
public class CreateInvoiceItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
