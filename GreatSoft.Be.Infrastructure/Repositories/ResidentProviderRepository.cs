using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class ResidentProviderRepository : Repository<ResidentProvider>, IResidentProviderRepository
{
    public ResidentProviderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ResidentProvider?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .Include(p => p.ProviderServiceType)
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public override async Task<ResidentProvider?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.ProviderServiceType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<ResidentProvider>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.ProviderServiceType)
            .ToListAsync();
    }
}


