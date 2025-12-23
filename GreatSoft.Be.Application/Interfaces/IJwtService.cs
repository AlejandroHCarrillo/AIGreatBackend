using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}

