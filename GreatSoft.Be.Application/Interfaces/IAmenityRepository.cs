using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IAmenityRepository : IRepository<Amenity>
{
    Task<IEnumerable<Amenity>> GetByCommunityIdAsync(int communityId);
    Task<Amenity?> GetByIdWithDetailsAsync(int id);
}

