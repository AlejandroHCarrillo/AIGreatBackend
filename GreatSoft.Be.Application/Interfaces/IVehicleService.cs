using GreatSoft.Be.Application.DTOs.Vehicle;

namespace GreatSoft.Be.Application.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllAsync();
    Task<VehicleDto?> GetByIdAsync(int id);
    Task<VehicleDto> CreateAsync(CreateVehicleDto createVehicleDto);
    Task<VehicleDto?> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto);
    Task<bool> DeleteAsync(int id);
}

