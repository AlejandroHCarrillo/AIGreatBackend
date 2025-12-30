using GreatSoft.Be.Application.DTOs.Role;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(MapToDto);
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;
        return MapToDto(role);
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto)
    {
        // Check if role name already exists
        var existingRole = await _roleRepository.GetByNameAsync(createRoleDto.Name);
        if (existingRole != null)
        {
            throw new InvalidOperationException("Role name already exists");
        }

        var role = new Role
        {
            Name = createRoleDto.Name,
            Description = createRoleDto.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _roleRepository.AddAsync(role);
        return MapToDto(role);
    }

    public async Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto updateRoleDto)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;

        role.Description = updateRoleDto.Description;
        role.IsActive = updateRoleDto.IsActive;
        role.UpdatedAt = DateTime.UtcNow;

        await _roleRepository.UpdateAsync(role);
        return MapToDto(role);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return false;

        await _roleRepository.DeleteAsync(role);
        return true;
    }

    private static RoleDto MapToDto(Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive
        };
    }
}

