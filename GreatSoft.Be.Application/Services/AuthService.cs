using GreatSoft.Be.Application.DTOs.Auth;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace GreatSoft.Be.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordService passwordService,
        IJwtService jwtService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    /// <summary>
    /// Convierte un int a Guid. Usa el int como parte del Guid.
    /// </summary>
    private static Guid IntToGuid(int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameOrEmailAsync(request.Username);
        if (user == null || !user.IsActive)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.Name);
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        // Por ahora, no hay información de residente separada
        // En el futuro, si se necesita, se puede agregar una relación
        ResidentInfoDto? residentInfo = null;

        return new LoginResponse
        {
            Token = token,
            UserId = IntToGuid(user.Id),
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.Name,
            ExpiresAt = expiresAt,
            ResidentInfo = residentInfo
        };
    }

    public async Task<LoginResponse?> RegisterAsync(RegisterRequest request)
    {
        // Check if user already exists
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return null;
        }

        // Verify role exists
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role == null || !role.IsActive)
        {
            return null;
        }

        // Create new user
        var user = new User
        {
            Username = request.Username ?? request.Email, // Usar Email como Username si no se proporciona
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            RoleId = request.RoleId,
            CompanyId = request.CompanyId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        // Get user with role for response
        var createdUser = await _userRepository.GetByIdWithRoleAsync(user.Id);
        if (createdUser == null)
        {
            return null;
        }

        var token = _jwtService.GenerateToken(createdUser.Id, createdUser.Email, createdUser.Role.Name);
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        return new LoginResponse
        {
            Token = token,
            UserId = IntToGuid(createdUser.Id),
            Username = createdUser.Username ?? createdUser.Email,
            Email = createdUser.Email,
            Role = createdUser.Role.Name,
            ExpiresAt = expiresAt,
            ResidentInfo = null // No hay residente en el registro inicial
        };
    }
}

