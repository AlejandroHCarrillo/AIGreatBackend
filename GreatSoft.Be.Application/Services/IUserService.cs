using GreatSoft.Be.Application.DTOs.User;

namespace GreatSoft.Be.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto> CreateUserAsync(CreateUserRequest request);
    Task<UserDto> UpdateUserAsync(Guid id, UpdateUserRequest request);
    Task<bool> DeleteUserAsync(Guid id);
}

