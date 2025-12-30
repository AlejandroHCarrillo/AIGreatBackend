using System.Security.Claims;

namespace GreatSoft.Be.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(int userId, string email, string role);
    ClaimsPrincipal? ValidateToken(string token);
}

