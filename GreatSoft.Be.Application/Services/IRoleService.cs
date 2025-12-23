using GreatSoft.Be.Application.DTOs.Role;

namespace GreatSoft.Be.Application.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto?> GetRoleByIdAsync(Guid id);
    Task<RoleDto> CreateRoleAsync(CreateRoleRequest request);
    Task<RoleDto> UpdateRoleAsync(Guid id, UpdateRoleRequest request);
    Task<bool> DeleteRoleAsync(Guid id);
}


