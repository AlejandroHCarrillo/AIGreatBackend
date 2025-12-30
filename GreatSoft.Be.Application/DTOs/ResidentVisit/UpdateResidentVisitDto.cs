namespace GreatSoft.Be.Application.DTOs.ResidentVisit;

public class UpdateResidentVisitDto
{
    public string VisitorName { get; set; } = string.Empty;
    public string? VisitorDocument { get; set; }
    public DateTime VisitDate { get; set; }
    public TimeSpan VisitTime { get; set; }
    public string? Purpose { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

