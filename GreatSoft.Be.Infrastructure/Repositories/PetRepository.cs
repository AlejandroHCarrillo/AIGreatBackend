using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class PetRepository : Repository<Pet>, IPetRepository
{
    public PetRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Pet?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Community)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pet>> GetByCommunityIdAsync(int communityId)
    {
        return await _dbSet
            .Include(p => p.Community)
            .Include(p => p.Owner)
            .Where(p => p.CommunityId == communityId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Pet>> GetByOwnerIdAsync(int ownerId)
    {
        return await _dbSet
            .Include(p => p.Community)
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }
}

