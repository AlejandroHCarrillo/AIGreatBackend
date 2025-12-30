using GreatSoft.Be.Application.DTOs.Role;

namespace GreatSoft.Be.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync();
    Task<RoleDto?> GetByIdAsync(int id);
    Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto);
    Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto updateRoleDto);
    Task<bool> DeleteAsync(int id);
}

