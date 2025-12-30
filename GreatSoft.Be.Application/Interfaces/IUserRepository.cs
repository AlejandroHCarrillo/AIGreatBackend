using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdWithRoleAsync(int id);
    Task<IEnumerable<User>> GetByCompanyIdAsync(int companyId);
    Task<IEnumerable<User>> GetByRoleIdAsync(int roleId);
}

