using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class ResidentProvider : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty; // Delivery, Maintenance, etc.
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int CommunityId { get; set; }

    // Navigation properties
    public virtual Community Community { get; set; } = null!;
}

