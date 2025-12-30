using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<Vehicle>> GetByCommunityIdAsync(int communityId);
    Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(int ownerId);
}

