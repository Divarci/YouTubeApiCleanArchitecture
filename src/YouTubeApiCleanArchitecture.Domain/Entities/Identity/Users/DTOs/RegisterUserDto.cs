using System.ComponentModel.DataAnnotations;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.DTOs;
public class RegisterUserDto
{
    [Required]
    [MaxLength(25)]
    public string Fullname { get; set; } = null!;

    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    public string? AdminKey { get; set; }
}
