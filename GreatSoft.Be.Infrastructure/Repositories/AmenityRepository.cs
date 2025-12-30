using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class AmenityRepository : Repository<Amenity>, IAmenityRepository
{
    public AmenityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Amenity?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(a => a.Community)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Amenity>> GetByCommunityIdAsync(int communityId)
    {
        return await _dbSet
            .Include(a => a.Community)
            .Where(a => a.CommunityId == communityId)
            .ToListAsync();
    }

    public async Task<Amenity?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(a => a.Community)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}

