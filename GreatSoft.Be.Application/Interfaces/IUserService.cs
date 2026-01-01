using GreatSoft.Be.Application.DTOs.User;

namespace GreatSoft.Be.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(CreateUserDto createUserDto);
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto);
    Task<bool> DeleteAsync(int id);
    Task<Guid?> GetResidentIdByUserIdAsync(Guid userId);
}

