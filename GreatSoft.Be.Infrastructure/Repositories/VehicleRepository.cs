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

    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _dbSet
            .Include(v => v.Resident)
            .Include(v => v.VehicleType)
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);
    }

    public override async Task<Vehicle?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(v => v.Resident)
            .Include(v => v.VehicleType)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public override async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _dbSet
            .Include(v => v.Resident)
            .Include(v => v.VehicleType)
            .ToListAsync();
    }
}


