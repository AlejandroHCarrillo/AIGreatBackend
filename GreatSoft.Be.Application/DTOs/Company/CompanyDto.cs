namespace GreatSoft.Be.Application.DTOs.Company;

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? TaxId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

