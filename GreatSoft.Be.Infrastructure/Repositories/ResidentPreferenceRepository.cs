using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class ResidentPreferenceRepository : Repository<ResidentPreference>, IResidentPreferenceRepository
{
    public ResidentPreferenceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<ResidentPreference?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(rp => rp.Resident)
            .FirstOrDefaultAsync(rp => rp.Id == id);
    }

    public async Task<IEnumerable<ResidentPreference>> GetByResidentIdAsync(int residentId)
    {
        return await _dbSet
            .Include(rp => rp.Resident)
            .Where(rp => rp.ResidentId == residentId)
            .ToListAsync();
    }

    public async Task<ResidentPreference?> GetByResidentIdAndNameAsync(int residentId, string name)
    {
        return await _dbSet
            .Include(rp => rp.Resident)
            .FirstOrDefaultAsync(rp => rp.ResidentId == residentId && rp.Name == name);
    }
}


