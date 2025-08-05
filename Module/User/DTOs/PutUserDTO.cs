using System.ComponentModel.DataAnnotations;

namespace Module.User.DTOs;

public class PutUserDTO
{
    [Required, RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "DOB must be in YYYY-MM-DD format.")]
    public string Dob { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Role { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;
}
