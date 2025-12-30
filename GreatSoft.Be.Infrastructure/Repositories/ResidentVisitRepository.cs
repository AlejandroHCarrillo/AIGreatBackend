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

    public override async Task<ResidentVisit?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(rv => rv.Community)
            .Include(rv => rv.Resident)
            .FirstOrDefaultAsync(rv => rv.Id == id);
    }

    public async Task<IEnumerable<ResidentVisit>> GetByCommunityIdAsync(int communityId)
    {
        return await _dbSet
            .Include(rv => rv.Community)
            .Include(rv => rv.Resident)
            .Where(rv => rv.CommunityId == communityId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ResidentVisit>> GetByResidentIdAsync(int residentId)
    {
        return await _dbSet
            .Include(rv => rv.Community)
            .Where(rv => rv.ResidentId == residentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ResidentVisit>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(rv => rv.Community)
            .Include(rv => rv.Resident)
            .Where(rv => rv.VisitDate >= startDate && rv.VisitDate <= endDate)
            .ToListAsync();
    }
}

