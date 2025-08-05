using Module.User.Repositories;

namespace Module.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<DTOs.UserDTO>> GetAllUsersAsync()
    {
        var users = await _repo.GetAllAsync();
        return users.Select(u => new DTOs.UserDTO
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Dob = u.Dob,
            Role = u.Role,
            Address = u.Address,
            CreatedAt = u.CreatedAt
        }).ToList();
    }

    public async Task<DTOs.UserDTO?> GetUserByIdAsync(Guid id)
    {
        var u = await _repo.GetByIdAsync(id);
        if (u == null) return null;

        return new DTOs.UserDTO
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Dob = u.Dob,
            Role = u.Role,
            Address = u.Address,
            CreatedAt = u.CreatedAt
        };
    }

    public async Task<DTOs.UserDTO> CreateUserAsync(DTOs.CreateUserDTO dto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new Entities.User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Password = hashedPassword,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repo.AddAsync(user);

        return new DTOs.UserDTO
        {
            Id = created.Id,
            Name = created.Name,
            Email = created.Email,
            Dob = created.Dob,
            Role = created.Role,
            Address = created.Address,
            CreatedAt = created.CreatedAt
        };
    }


    public async Task<bool> SeedAsync()
    {
        var predefinedUsers = new List<Entities.User>
              {
                  new Entities.User {
                            Name = "admin",
                            Email = "admin@example.com",
                            Password = "Admin123!",
                            Dob = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc),
                            Role = "Manager",
                            Address = "USA",
                        },
                  new Entities.User {
                            Name = "john_doe",
                            Email = "john@example.com",
                            Password = "John123!",
                            Dob = DateTime.SpecifyKind(new DateTime(1991, 2, 2), DateTimeKind.Utc),
                            Role = "Director",
                            Address = "Germany",
                        },
                  new Entities.User {
                            Name = "jane_doe",
                            Email = "jane@example.com",
                            Password = "Jane123!",
                            Dob = DateTime.SpecifyKind(new DateTime(1992, 3, 3), DateTimeKind.Utc),
                            Role = "Employee",
                            Address = "France",
                        },
                  new Entities.User {
                            Name = "charlie_doe",
                            Email = "charlie@example.com",
                            Password = "Charlie123!",
                            Dob = DateTime.SpecifyKind(new DateTime(1992, 3, 3), DateTimeKind.Utc),
                            Role = "CEO",
                            Address = "Polish",
                        },
                  new Entities.User {
                            Name = "bob_doe",
                            Email = "bob@example.com",
                            Password = "Bob123!",
                            Dob = DateTime.SpecifyKind(new DateTime(1992, 3, 3), DateTimeKind.Utc),
                            Role = "CTO",
                            Address = "Russia",
                        },
                  new Entities.User {
                            Name = "alice_doe",
                            Email = "alice@example.com",
                            Password = "alice123!",
                            Dob = DateTime.SpecifyKind(new DateTime(1992, 3, 3), DateTimeKind.Utc),
                            Role = "Software Engineer",
                            Address = "Canada",
                        },
              };

        foreach (var user in predefinedUsers)
        {
            await _repo.AddAsync(user);
        }
        return true;
    }

    public async Task<bool> LoginAsync(DTOs.LoginUserDTO dto)
    {
        // Get the hashed password from the repo
        var user = await _repo.GetUserByNameAsync(dto.Name);

        if (user == null)
            return false;

        // Verify entered password against stored hash
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password.ToString());

        return isPasswordValid;
    }


    public async Task<bool> UpdatePasswordAsync(DTOs.UpdatePasswordDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Password))
            return false;

        var user = await _repo.GetUserByNameAsync(dto.Name);
        if (user == null)
            return false;

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
        if (!isPasswordValid)
            return false;

        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

        await _repo.UpdateAsync(user);
        return true;
    }

    public async Task<bool> PatchUserAsync(DTOs.PatchUserDTO dto, string name)
    {
        var user = await _repo.GetUserByNameAsync(name);
        if (user == null)
            return false;

        // Update DOB if provided and valid
        if (!string.IsNullOrWhiteSpace(dto.Dob))
        {
            if (!DateTime.TryParse(dto.Dob, out var parsedDob))
                return false; // Invalid date format

            user.Dob = DateTime.SpecifyKind(parsedDob, DateTimeKind.Utc);
        }

        // Update Role if provided
        if (!string.IsNullOrWhiteSpace(dto.Role))
        {
            user.Role = dto.Role;
        }

        // Update Address if provided
        if (!string.IsNullOrWhiteSpace(dto.Address))
        {
            user.Address = dto.Address;
        }

        await _repo.UpdateAsync(user);
        return true;
    }

    public async Task<bool> PutUserAsync(DTOs.PutUserDTO dto, string name)
    {
        var user = await _repo.GetUserByNameAsync(name);
        if (user == null)
            return false;

        // Update DOB if provided and valid
        if (!string.IsNullOrWhiteSpace(dto.Dob))
        {
            if (!DateTime.TryParse(dto.Dob, out var parsedDob))
                return false; // Invalid date format

            user.Dob = DateTime.SpecifyKind(parsedDob, DateTimeKind.Utc);
        }

        await _repo.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteUserAsync(DTOs.DeleteUserDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Password))
            return false;

        var user = await _repo.GetUserByNameAsync(dto.Name);
        if (user == null)
            return false;

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
        if (!isPasswordValid)
            return false;

        await _repo.DeleteAsync(dto.Name);
        return true;
    }


}

