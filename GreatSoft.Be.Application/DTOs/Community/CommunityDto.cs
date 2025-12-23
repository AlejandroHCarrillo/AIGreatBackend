namespace GreatSoft.Be.Application.DTOs.Community;

public class CommunityDto
{
    public Guid Id { get; set; }
    public Guid CommunityTypeId { get; set; }
    public string CommunityTypeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Lat { get; set; }
    public int Lng { get; set; }
    public int HousingCount { get; set; }
    public string ContactPhone { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}


