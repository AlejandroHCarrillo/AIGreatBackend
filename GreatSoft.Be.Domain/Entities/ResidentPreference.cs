using GreatSoft.Be.Domain.Common;

namespace GreatSoft.Be.Domain.Entities;

public class ResidentPreference : BaseEntity
{
    public int ResidentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    // Navigation properties
    public virtual User Resident { get; set; } = null!;
}


