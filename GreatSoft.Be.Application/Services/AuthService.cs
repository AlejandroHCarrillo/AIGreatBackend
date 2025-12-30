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

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !user.IsActive)
        {
            return null;
        }

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return null;
        }

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.Name);
        var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60");

        return new LoginResponse
        {
            Token = token,
            Email = user.Email,
            Role = user.Role.Name,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes)
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

        return new LoginResponse
        {
            Token = token,
            Email = createdUser.Email,
            Role = createdUser.Role.Name,
            UserId = createdUser.Id,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes)
        };
    }
}

