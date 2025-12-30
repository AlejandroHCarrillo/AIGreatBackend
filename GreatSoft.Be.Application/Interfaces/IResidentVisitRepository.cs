using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Interfaces;

public interface IResidentVisitRepository : IRepository<ResidentVisit>
{
    Task<IEnumerable<ResidentVisit>> GetByCommunityIdAsync(int communityId);
    Task<IEnumerable<ResidentVisit>> GetByResidentIdAsync(int residentId);
    Task<IEnumerable<ResidentVisit>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}

