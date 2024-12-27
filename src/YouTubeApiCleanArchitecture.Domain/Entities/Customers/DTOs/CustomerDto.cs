using System.ComponentModel.DataAnnotations;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
public abstract class BaseCustomerDto
{
    [Required]
    [MaxLength(45)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(40)]
    public string FirstLineAddress { get; set; } = null!;

    [MaxLength(40)]
    public string? SecondLineLineAddress { get; set; }

    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string City { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Country { get; set; } = null!;
}

public class CreateCustomerDto : BaseCustomerDto;
public class UpdateCustomerDto : BaseCustomerDto;