namespace GreatSoft.Be.Application.DTOs.ResidentProvider;

public class ResidentProviderDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int CommunityId { get; set; }
    public string CommunityName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

