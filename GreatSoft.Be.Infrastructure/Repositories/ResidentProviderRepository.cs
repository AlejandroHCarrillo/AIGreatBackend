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

    public override async Task<ResidentProvider?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(rp => rp.Community)
            .FirstOrDefaultAsync(rp => rp.Id == id);
    }

    public async Task<IEnumerable<ResidentProvider>> GetByCommunityIdAsync(int communityId)
    {
        return await _dbSet
            .Include(rp => rp.Community)
            .Where(rp => rp.CommunityId == communityId)
            .ToListAsync();
    }
}

