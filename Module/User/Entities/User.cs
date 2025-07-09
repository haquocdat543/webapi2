using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.User.Entities;

[Table("user")]
public class User
{
  [Key]
  public Guid Id { get; set; }

  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;

  [Required]
  public string Password { get; set; } = string.Empty;

  public DateTime? Dob { get; set; }

  public string? Role { get; set; }

  public string? Address { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public DateTime? DeletedAt { get; set; }
}

