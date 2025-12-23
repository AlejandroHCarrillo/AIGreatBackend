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

    public override async Task<Pet?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Resident)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Resident)
            .ToListAsync();
    }
}


