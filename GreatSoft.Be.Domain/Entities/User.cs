namespace GreatSoft.Be.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
    
    // CompanyUsers relationship (many-to-many through CompanyUser)
    public ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
    
    // ResidentUser relationship (one-to-one)
    public ResidentUser? ResidentUser { get; set; }
}

