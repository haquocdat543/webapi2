
namespace Module.User.Services;

public interface IUserService
{
    Task<List<DTOs.UserDTO>> GetAllUsersAsync();
    Task<DTOs.UserDTO?> GetUserByIdAsync(Guid id);
    Task<DTOs.UserDTO> CreateUserAsync(DTOs.CreateUserDTO dto);
    Task<bool> UpdatePasswordAsync(DTOs.UpdatePasswordDTO dto);
    Task<bool> SeedAsync();
    Task<bool> LoginAsync(DTOs.LoginUserDTO dto);
    Task<bool> PatchUserAsync(DTOs.PatchUserDTO dto, string name);
    Task<bool> DeleteUserAsync(DTOs.DeleteUserDTO dto);
}

