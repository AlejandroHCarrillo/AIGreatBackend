namespace GreatSoft.Be.Application.DTOs.ResidentVisit;

public class ResidentVisitDto
{
    public int Id { get; set; }
    public string VisitorName { get; set; } = string.Empty;
    public string? VisitorDocument { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan VisitTime { get; set; }
    public int CommunityId { get; set; }
    public string CommunityName { get; set; } = string.Empty;
    public int ResidentId { get; set; }
    public string ResidentName { get; set; } = string.Empty;
    public string? Purpose { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

