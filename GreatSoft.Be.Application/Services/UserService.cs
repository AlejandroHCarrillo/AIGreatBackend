using GreatSoft.Be.Application.DTOs.User;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPasswordService _passwordService;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ICompanyRepository companyRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _companyRepository = companyRepository;
        _passwordService = passwordService;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToDto);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdWithRoleAsync(id);
        if (user == null) return null;
        return MapToDto(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
    {
        // Check if email already exists
        var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        // Verify role exists
        var role = await _roleRepository.GetByIdAsync(createUserDto.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        // Verify company exists if provided
        if (createUserDto.CompanyId.HasValue)
        {
            var company = await _companyRepository.GetByIdAsync(createUserDto.CompanyId.Value);
            if (company == null)
            {
                throw new InvalidOperationException("Company not found");
            }
        }

        var user = new User
        {
            Username = createUserDto.Username ?? createUserDto.Email, // Usar Email como Username si no se proporciona
            Email = createUserDto.Email,
            PasswordHash = _passwordService.HashPassword(createUserDto.Password),
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Phone = createUserDto.Phone,
            RoleId = createUserDto.RoleId,
            CompanyId = createUserDto.CompanyId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        var createdUser = await _userRepository.GetByIdWithRoleAsync(user.Id);
        return MapToDto(createdUser!);
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        // Verify role exists
        var role = await _roleRepository.GetByIdAsync(updateUserDto.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        // Verify company exists if provided
        if (updateUserDto.CompanyId.HasValue)
        {
            var company = await _companyRepository.GetByIdAsync(updateUserDto.CompanyId.Value);
            if (company == null)
            {
                throw new InvalidOperationException("Company not found");
            }
        }

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.Phone = updateUserDto.Phone;
        user.RoleId = updateUserDto.RoleId;
        user.CompanyId = updateUserDto.CompanyId;
        user.IsActive = updateUserDto.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        var updatedUser = await _userRepository.GetByIdWithRoleAsync(user.Id);
        return MapToDto(updatedUser!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    public Task<Guid?> GetResidentIdByUserIdAsync(Guid userId)
    {
        // Por ahora, retornar null ya que no hay una relación directa con Resident
        // En el futuro, si se necesita, se puede implementar
        return Task.FromResult<Guid?>(null);
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            RoleId = user.RoleId,
            RoleName = user.Role?.Name ?? string.Empty,
            CompanyId = user.CompanyId,
            CompanyName = user.Company?.Name,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}

