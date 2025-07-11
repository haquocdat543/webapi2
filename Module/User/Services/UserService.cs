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

