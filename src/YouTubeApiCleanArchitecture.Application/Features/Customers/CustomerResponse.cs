using AutoMapper;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.ValueObject;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers;
public class CustomerResponse : IResult
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public decimal Balance { get; set; }
}

public class CustomerResponseCollection : IResult
{
    public IReadOnlyCollection<CustomerResponse> Customers { get; set; } = null!;
}

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, CustomerResponse>()
            .ForMember(dto => dto.Title, opt => opt.MapFrom(ent => ent.Title.Value))
            .ForMember(dto => dto.Balance, opt => opt.MapFrom(ent => ent.Balance.Value));
    }
}

