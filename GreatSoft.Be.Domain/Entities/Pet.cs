using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class Pet : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Dog, Cat, etc.
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public int CommunityId { get; set; }
    public int OwnerId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual Community Community { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
}

