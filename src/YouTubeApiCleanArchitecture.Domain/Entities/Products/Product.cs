using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Products;
public sealed class Product : BaseEntity
{
    private Product() { }

    private Product(
        Guid id,
        Title description,
        Money unitPrice) : base(id)
    {
        Description = description;
        UnitPrice = unitPrice;
    }

    public Title Description { get; private set; } = null!;

    public Money UnitPrice { get; private set; } = null!;

    public static Product Create(CreateProductDto dto)
        => new (
            Guid.NewGuid(),
            new Title(dto.Description),
            new Money(dto.UnitPrice));

    public void Update(UpdateProductDto dto)
    {
        Description = new Title(dto.Description);
        UnitPrice = new Money(dto.UnitPrice);
    }
}

