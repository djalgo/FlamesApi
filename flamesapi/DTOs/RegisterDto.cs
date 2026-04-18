using System.ComponentModel.DataAnnotations;

public record RegisterDto (
    [Required]
    string Username,
    [Required]
    string KnownAs,
    [Required]
    string Gender,
    [Required]
    DateTime DateOfBirth,
    [Required]
    string City,
    [Required]
    string Country,
    [Required]
    [StringLength(8, MinimumLength = 4)]
    string Password
    );