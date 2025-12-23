namespace GreatSoft.Be.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

