using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Features.Products;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.ValueObject;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers;
public class CustomerResponse : IResult
{
    public Guid Id { get; set; }

    public string Title { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public decimal Balance { get; private set; }
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

