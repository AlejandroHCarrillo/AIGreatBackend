using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(v => v.Community)
            .Include(v => v.Owner)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _dbSet
            .Include(v => v.Community)
            .Include(v => v.Owner)
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
    }

    public async Task<IEnumerable<Vehicle>> GetByCommunityIdAsync(int communityId)
    {
        return await _dbSet
            .Include(v => v.Community)
            .Include(v => v.Owner)
            .Where(v => v.CommunityId == communityId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(int ownerId)
    {
        return await _dbSet
            .Include(v => v.Community)
            .Where(v => v.OwnerId == ownerId)
            .ToListAsync();
    }
}

