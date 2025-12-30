using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Community>> GetByCompanyIdAsync(int companyId)
    {
        return await _dbSet
            .Include(c => c.Company)
            .Where(c => c.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<Community?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}

