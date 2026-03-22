using System.ComponentModel.DataAnnotations;

public record RegisterDto (
    [Required]
    string Username, 
    [Required]
    string Password
    );