using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class ResidentVisitRepository : Repository<ResidentVisit>, IResidentVisitRepository
{
    public ResidentVisitRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<ResidentVisit?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(v => v.Resident)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public override async Task<IEnumerable<ResidentVisit>> GetAllAsync()
    {
        return await _dbSet
            .Include(v => v.Resident)
            .ToListAsync();
    }
}


