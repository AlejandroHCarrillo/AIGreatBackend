namespace GreatSoft.Be.Application.DTOs.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public List<CompanyInfoDto> Companies { get; set; } = new List<CompanyInfoDto>();
}

public class CompanyInfoDto
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
}

