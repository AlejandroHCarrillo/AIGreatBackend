namespace GreatSoft.Be.Domain.Entities;

public class ResidentUser
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ResidentId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Resident Resident { get; set; } = null!;
}


