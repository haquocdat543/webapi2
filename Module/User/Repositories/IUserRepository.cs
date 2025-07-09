namespace Module.User.Repositories;

public interface IUserRepository
{
  Task<List<Entities.User>> GetAllAsync();
  Task<Entities.User?> GetByIdAsync(Guid id);
  Task<Entities.User?> GetByEmailAsync(string email);
  Task<Entities.User> AddAsync(Entities.User user);
  Task UpdateAsync(Entities.User user);
  Task DeleteAsync(Entities.User user);
}

