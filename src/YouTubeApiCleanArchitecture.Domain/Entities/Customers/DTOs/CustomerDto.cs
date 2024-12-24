namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
public abstract class BaseCustomerDto
{
    public string Title { get; set; } = null!;
    public string FirstLineAddress { get; set; } = null!;
    public string? SecondLineLineAddress { get; set; }
    public string Postcode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
}

public class CreateCustomerDto : BaseCustomerDto;
public class UpdateCustomerDto : BaseCustomerDto
{
    public Guid CustomerId { get; set; }
}
