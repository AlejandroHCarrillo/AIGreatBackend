namespace GreatSoft.Be.Application.DTOs.ResidentVisit;

public class UpdateResidentVisitRequest
{
    public Guid ResidentId { get; set; }
    public string VisitorName { get; set; } = string.Empty;
    public int TotalPeople { get; set; }
    public string? VehicleColor { get; set; }
    public string? LicensePlate { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime ArrivalDate { get; set; }
    public DateTime? DepartureDate { get; set; }
}


