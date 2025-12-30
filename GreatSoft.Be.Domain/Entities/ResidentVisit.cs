using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class ResidentVisit : BaseEntity
{
    public string VisitorName { get; set; } = string.Empty;
    public string? VisitorDocument { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan VisitTime { get; set; }
    public int CommunityId { get; set; }
    public int ResidentId { get; set; }
    public string? Purpose { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Completed

    // Navigation properties
    public virtual Community Community { get; set; } = null!;
    public virtual User Resident { get; set; } = null!;
}

