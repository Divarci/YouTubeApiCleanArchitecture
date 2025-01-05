using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Features.Customers;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices;
public class InvoiceResponse : IResult
{
    public Guid Id { get; set; }

    public string PoNumber { get; set; } = null!;

    public CustomerResponse Customer { get; set; } = null!;

    public ICollection<InvoiceItemResponse> PurchasedProducts { get; set; } = null!;

    public decimal InvoiceBalance { get; set; }

}

public class InvoiceResponseCollection : IResult
{
    public IReadOnlyCollection<InvoiceResponse> Invoices { get; set; } = null!;
}

public class InvoiceMapper : Profile
{
    public InvoiceMapper()
    {
        CreateMap<Invoice, InvoiceResponse>()
            .ForMember(dto => dto.PoNumber, opt => opt.MapFrom(ent => ent.PoNumber.Value))
            .ForMember(dto => dto.Customer, opt => opt.MapFrom(ent => ent.Customer))
            .ForMember(dto => dto.InvoiceBalance, opt => opt.MapFrom(ent => ent.TotalBalance.Value));
    }
}