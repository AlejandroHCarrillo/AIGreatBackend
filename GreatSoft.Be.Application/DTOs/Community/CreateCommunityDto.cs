namespace GreatSoft.Be.Application.DTOs.Community;

public class CreateCommunityDto
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public int CompanyId { get; set; }
}

