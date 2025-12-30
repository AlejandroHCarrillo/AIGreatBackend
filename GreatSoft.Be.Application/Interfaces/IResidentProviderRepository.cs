using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IResidentProviderRepository : IRepository<ResidentProvider>
{
    Task<IEnumerable<ResidentProvider>> GetByCommunityIdAsync(int communityId);
}

