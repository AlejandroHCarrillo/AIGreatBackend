namespace GreatSoft.Be.Domain.Entities;

public class ResidentVisit
{
    public Guid Id { get; set; }
    public Guid ResidentId { get; set; } // ID del propietario
    public string VisitorName { get; set; } = string.Empty; // Nombre del visitante
    public int TotalPeople { get; set; } // Total de Personas
    public string? VehicleColor { get; set; } // Color del auto (opcional)
    public string? LicensePlate { get; set; } // Placas del auto (opcional)
    public string Subject { get; set; } = string.Empty; // Asunto
    public DateTime ArrivalDate { get; set; } // Fecha de llegada
    public DateTime? DepartureDate { get; set; } // Fecha de salida (opcional, puede estar en curso)
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public Resident Resident { get; set; } = null!;
}


