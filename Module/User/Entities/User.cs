using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Module.User.Entities;

[EntityTypeConfiguration(typeof(User.UserEntityConfiguration))]
public class User
{
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

  // Inline Fluent API Configuration class
  private class UserEntityConfiguration : IEntityTypeConfiguration<User>
  {
	public void Configure(EntityTypeBuilder<User> entity)
	{
	  entity.ToTable("user");

	  entity.HasKey(u => u.Id);

	  entity.Property(u => u.Name).IsRequired();
	  entity.HasIndex(u => u.Name).IsUnique();

	  entity.Property(u => u.Email).IsRequired();
	  entity.HasIndex(u => u.Email).IsUnique();

	  entity.Property(u => u.Password).IsRequired();
	  // No unique index on password (not recommended)

	  entity.Property(u => u.Dob);
	  entity.Property(u => u.Role);
	  entity.Property(u => u.Address);

	  entity.Property(u => u.CreatedAt)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.IsRequired();

	  entity.Property(u => u.UpdatedAt)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.IsRequired(false);

	  entity.Property(u => u.DeletedAt);
	}
  }
}

