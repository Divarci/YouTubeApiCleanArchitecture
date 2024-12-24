namespace YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;
public abstract class BaseProductDto
{
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
}

public class CreateProductDto : BaseProductDto;

public class UpdateProductDto : BaseProductDto
{
    public Guid ProductId { get; set; }
}


