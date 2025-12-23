namespace GreatSoft.Be.Domain.Entities;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Nombre
    public string Species { get; set; } = string.Empty; // Especie
    public string Breed { get; set; } = string.Empty; // Raza
    public int Age { get; set; } // Edad en años
    public string Color { get; set; } = string.Empty; // Color predominante
    public Guid ResidentId { get; set; } // ID del residente propietario
    public DateTime CreatedAt { get; set; }
    
    // Navigation property
    public Resident Resident { get; set; } = null!;
}


