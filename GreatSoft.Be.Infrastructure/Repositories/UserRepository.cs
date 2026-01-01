using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdWithRoleAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
    }
    
    public async Task<User?> GetByIdWithResidentAsync(Guid id)
    {
        // Convertir Guid a int para buscar en la base de datos
        // Nota: Esta conversión es temporal, idealmente User.Id debería ser Guid
        var bytes = id.ToByteArray();
        var intId = BitConverter.ToInt32(bytes, 0);
        return await _dbSet
            .Include(u => u.Role)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.Id == intId);
    }

    public async Task<IEnumerable<User>> GetByCompanyIdAsync(int companyId)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Where(u => u.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetByRoleIdAsync(int roleId)
    {
        return await _dbSet
            .Include(u => u.Role)
            .Where(u => u.RoleId == roleId)
            .ToListAsync();
    }
}

