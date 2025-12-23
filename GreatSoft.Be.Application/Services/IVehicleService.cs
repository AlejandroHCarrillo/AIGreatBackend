using GreatSoft.Be.Application.DTOs.Vehicle;

namespace GreatSoft.Be.Application.Services;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
    Task<VehicleDto?> GetVehicleByIdAsync(Guid id);
    Task<IEnumerable<VehicleDto>> GetVehiclesByResidentIdAsync(Guid residentId);
    Task<VehicleDto> CreateVehicleAsync(CreateVehicleRequest request);
    Task<VehicleDto> UpdateVehicleAsync(Guid id, UpdateVehicleRequest request);
    Task<bool> DeleteVehicleAsync(Guid id);
}


