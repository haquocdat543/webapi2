
namespace Module.User.Services;

public interface IUserService
{
    Task<List<DTOs.UserDTO>> GetAllUsersAsync();
    Task<DTOs.UserDTO?> GetUserByIdAsync(Guid id);
    Task<DTOs.UserDTO> CreateUserAsync(DTOs.CreateUserDTO dto);
    Task<bool> UpdatePasswordAsync(DTOs.UpdatePasswordDTO dto);
    Task<bool> LoginAsync(DTOs.LoginUserDTO dto);
    Task<bool> DeleteUserAsync(DTOs.DeleteUserDTO dto);
}

