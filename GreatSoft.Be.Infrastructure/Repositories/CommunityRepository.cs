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

    public async Task<Community?> GetByNameAsync(string name)
    {
        return await _dbSet
            .Include(c => c.CommunityType)
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public override async Task<Community?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(c => c.CommunityType)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<IEnumerable<Community>> GetAllAsync()
    {
        return await _dbSet
            .Include(c => c.CommunityType)
            .ToListAsync();
    }
}


