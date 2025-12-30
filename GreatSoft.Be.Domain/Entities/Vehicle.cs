using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string LicensePlate { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public int? Year { get; set; }
    public int CommunityId { get; set; }
    public int OwnerId { get; set; }

    // Navigation properties
    public virtual Community Community { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
}

