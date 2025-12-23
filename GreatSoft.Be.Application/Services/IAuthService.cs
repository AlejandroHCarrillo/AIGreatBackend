using GreatSoft.Be.Application.DTOs.Auth;
using GreatSoft.Be.Application.DTOs.User;

namespace GreatSoft.Be.Application.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<UserDto> RegisterAsync(RegisterRequest request);
    Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
}


