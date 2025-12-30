using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class Amenity : BaseEntity
{
    public int CommunityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Rules { get; set; }
    public decimal? Cost { get; set; }
    public string? Image { get; set; }
    public int? MaxCapacity { get; set; }
    public int? SimultaneousReservations { get; set; }

    // Navigation properties
    public virtual Community Community { get; set; } = null!;
}

