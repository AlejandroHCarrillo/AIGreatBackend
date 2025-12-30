namespace GreatSoft.Be.Application.DTOs.ResidentVisit;

public class CreateResidentVisitDto
{
    public string VisitorName { get; set; } = string.Empty;
    public string? VisitorDocument { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan VisitTime { get; set; }
    public int CommunityId { get; set; }
    public int ResidentId { get; set; }
    public string? Purpose { get; set; }
}

