namespace GreatSoft.Be.Domain.Entities;

public class CompanyUser
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public Company Company { get; set; } = null!;
    public User User { get; set; } = null!;
}


