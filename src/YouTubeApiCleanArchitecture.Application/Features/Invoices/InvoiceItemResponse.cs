using AutoMapper;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices;

public class InvoiceItemResponse
{
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

public class InvoiceItemMapper : Profile
{
    public InvoiceItemMapper()
    {
        CreateMap<InvoiceItem, InvoiceItemResponse>()
            .ForMember(dto => dto.Description, opt => opt.MapFrom(ent => ent.Description.Value))
            .ForMember(dto => dto.UnitPrice, opt => opt.MapFrom(ent => ent.SellPrice.Value))
            .ForMember(dto => dto.Quantity, opt => opt.MapFrom(ent => ent.Quantity.Value))
            .ForMember(dto => dto.TotalPrice, opt => opt.MapFrom(ent => ent.TotalPrice.Value));
    }
}
