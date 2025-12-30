using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IPetRepository : IRepository<Pet>
{
    Task<IEnumerable<Pet>> GetByCommunityIdAsync(int communityId);
    Task<IEnumerable<Pet>> GetByOwnerIdAsync(int ownerId);
}

