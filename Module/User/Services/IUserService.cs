
namespace Module.User.Services;

public interface IUserService
{
  Task<List<DTOs.UserDTO>> GetAllUsersAsync();
  Task<DTOs.UserDTO?> GetUserByIdAsync(Guid id);
  Task<DTOs.UserDTO> CreateUserAsync(DTOs.CreateUserDTO dto);
  Task<bool> DeleteUserAsync(Guid id);
}

