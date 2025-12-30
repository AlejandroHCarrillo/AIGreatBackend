namespace GreatSoft.Be.Application.DTOs.ResidentProvider;

public class CreateResidentProviderDto
{
    public string Name { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int CommunityId { get; set; }
}

