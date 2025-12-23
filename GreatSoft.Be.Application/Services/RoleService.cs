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

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            RoleType = r.RoleType,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<RoleDto?> GetRoleByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            RoleType = role.RoleType,
            CreatedAt = role.CreatedAt
        };
    }

    public async Task<RoleDto> CreateRoleAsync(CreateRoleRequest request)
    {
        if (await _roleRepository.GetByNameAsync(request.Name) != null)
        {
            throw new InvalidOperationException("Role name already exists");
        }

        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            RoleType = request.RoleType,
            CreatedAt = DateTime.UtcNow
        };

        await _roleRepository.AddAsync(role);

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            RoleType = role.RoleType,
            CreatedAt = role.CreatedAt
        };
    }

    public async Task<RoleDto> UpdateRoleAsync(Guid id, UpdateRoleRequest request)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        var existingRole = await _roleRepository.GetByNameAsync(request.Name);
        if (existingRole != null && existingRole.Id != id)
        {
            throw new InvalidOperationException("Role name already exists");
        }

        role.Name = request.Name;
        role.Description = request.Description;
        role.RoleType = request.RoleType;

        await _roleRepository.UpdateAsync(role);

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            RoleType = role.RoleType,
            CreatedAt = role.CreatedAt
        };
    }

    public async Task<bool> DeleteRoleAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return false;
        }

        await _roleRepository.DeleteAsync(role);
        return true;
    }
}


