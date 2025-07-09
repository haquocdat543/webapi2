namespace Module.User.DTOs;

public class UserDTO
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public DateTime? Dob { get; set; }
  public string? Role { get; set; }
  public string? Address { get; set; }
  public DateTime CreatedAt { get; set; }
}

