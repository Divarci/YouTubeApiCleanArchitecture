using AutoMapper;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices;

public class InvoiceItemResponse
{
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class InvoiceItemMapper : Profile
{
    public InvoiceItemMapper()
    {
        CreateMap<InvoiceItem, InvoiceItemResponse>()
            .ForMember(dto => dto.Description, opt => opt.MapFrom(ent => ent.Description.Value))
            .ForMember(dto => dto.Quantity, opt => opt.MapFrom(ent => ent.Quantity.Value))
            .ForMember(dto => dto.Price, opt => opt.MapFrom(ent => ent.TotalPrice));
    }
}
