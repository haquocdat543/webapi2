using System.ComponentModel.DataAnnotations;

namespace Module.User.DTOs;

public class CreateUserDTO
{
    [Required, MinLength(3), MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8), MaxLength(20)]
    public string Password { get; set; } = string.Empty;
}

