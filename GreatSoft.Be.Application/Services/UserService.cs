using GreatSoft.Be.Application.DTOs.User;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUserRepository _companyUserRepository;
    private readonly IPasswordService _passwordService;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ICompanyRepository companyRepository,
        ICompanyUserRepository companyUserRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _companyRepository = companyRepository;
        _companyUserRepository = companyUserRepository;
        _passwordService = passwordService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Username = u.Username,
            Email = u.Email,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt,
            RoleId = u.RoleId,
            RoleName = u.Role?.Name ?? string.Empty,
            Companies = u.CompanyUsers.Select(cu => new CompanyInfoDto
            {
                CompanyId = cu.CompanyId,
                CompanyName = cu.Company?.Name ?? string.Empty
            }).ToList()
        });
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            RoleId = user.RoleId,
            RoleName = user.Role?.Name ?? string.Empty,
            Companies = user.CompanyUsers.Select(cu => new CompanyInfoDto
            {
                CompanyId = cu.CompanyId,
                CompanyName = cu.Company?.Name ?? string.Empty
            }).ToList()
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
    {
        if (await _userRepository.GetByUsernameAsync(request.Username) != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        if (await _userRepository.GetByEmailAsync(request.Email) != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password),
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            RoleId = request.RoleId
        };

        await _userRepository.AddAsync(user);

        // Create CompanyUser relationship if CompanyId is provided
        if (request.CompanyId.HasValue)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId.Value);
            if (company == null)
            {
                throw new InvalidOperationException("Company not found");
            }

            // Check if relationship already exists
            if (!await _companyUserRepository.ExistsAsync(request.CompanyId.Value, user.Id))
            {
                var companyUser = new CompanyUser
                {
                    Id = Guid.NewGuid(),
                    CompanyId = request.CompanyId.Value,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow
                };
                await _companyUserRepository.AddAsync(companyUser);
            }
        }

        // Reload user with CompanyUsers
        var reloadedUser = await _userRepository.GetByIdAsync(user.Id);
        if (reloadedUser == null)
        {
            throw new InvalidOperationException("Failed to reload user after creation");
        }

        return new UserDto
        {
            Id = reloadedUser.Id,
            FirstName = reloadedUser.FirstName,
            LastName = reloadedUser.LastName,
            Username = reloadedUser.Username,
            Email = reloadedUser.Email,
            IsActive = reloadedUser.IsActive,
            CreatedAt = reloadedUser.CreatedAt,
            RoleId = reloadedUser.RoleId,
            RoleName = role.Name,
            Companies = reloadedUser.CompanyUsers.Select(cu => new CompanyInfoDto
            {
                CompanyId = cu.CompanyId,
                CompanyName = cu.Company?.Name ?? string.Empty
            }).ToList()
        };
    }

    public async Task<UserDto> UpdateUserAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.RoleId = request.RoleId;
        user.IsActive = request.IsActive;

        await _userRepository.UpdateAsync(user);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            RoleId = user.RoleId,
            RoleName = role.Name,
            Companies = user.CompanyUsers.Select(cu => new CompanyInfoDto
            {
                CompanyId = cu.CompanyId,
                CompanyName = cu.Company?.Name ?? string.Empty
            }).ToList()
        };
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }

        await _userRepository.DeleteAsync(user);
        return true;
    }
}

