using GreatSoft.Be.Application.DTOs.Auth;

namespace GreatSoft.Be.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<LoginResponse?> RegisterAsync(RegisterRequest request);
}

