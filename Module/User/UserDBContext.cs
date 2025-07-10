using Microsoft.EntityFrameworkCore;

namespace Module.User;

public class UserDbContext : DbContext
{
  public UserDbContext(DbContextOptions<UserDbContext> options)
	  : base(options) { }

  public DbSet<Entities.User> Users => Set<Entities.User>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
	// No need to apply manual configuration — handled via [EntityTypeConfiguration]
	base.OnModelCreating(modelBuilder);
  }
}

