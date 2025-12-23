using GreatSoft.Be.Application.DTOs.Vehicle;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRepository<Resident> _residentRepository;
    private readonly IRepository<VehicleType> _vehicleTypeRepository;

    public VehicleService(
        IVehicleRepository vehicleRepository,
        IRepository<Resident> residentRepository,
        IRepository<VehicleType> vehicleTypeRepository)
    {
        _vehicleRepository = vehicleRepository;
        _residentRepository = residentRepository;
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles.Select(v => new VehicleDto
        {
            Id = v.Id,
            ResidentId = v.ResidentId,
            ResidentName = v.Resident?.FullName ?? string.Empty,
            Brand = v.Brand,
            VehicleTypeId = v.VehicleTypeId,
            VehicleTypeName = v.VehicleType?.Name ?? string.Empty,
            Model = v.Model,
            Year = v.Year,
            Color = v.Color,
            LicensePlate = v.LicensePlate,
            CreatedAt = v.CreatedAt
        });
    }

    public async Task<VehicleDto?> GetVehicleByIdAsync(Guid id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return null;

        return new VehicleDto
        {
            Id = vehicle.Id,
            ResidentId = vehicle.ResidentId,
            ResidentName = vehicle.Resident?.FullName ?? string.Empty,
            Brand = vehicle.Brand,
            VehicleTypeId = vehicle.VehicleTypeId,
            VehicleTypeName = vehicle.VehicleType?.Name ?? string.Empty,
            Model = vehicle.Model,
            Year = vehicle.Year,
            Color = vehicle.Color,
            LicensePlate = vehicle.LicensePlate,
            CreatedAt = vehicle.CreatedAt
        };
    }

    public async Task<IEnumerable<VehicleDto>> GetVehiclesByResidentIdAsync(Guid residentId)
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles
            .Where(v => v.ResidentId == residentId)
            .Select(v => new VehicleDto
            {
                Id = v.Id,
                ResidentId = v.ResidentId,
                ResidentName = v.Resident?.FullName ?? string.Empty,
                Brand = v.Brand,
                VehicleTypeId = v.VehicleTypeId,
                VehicleTypeName = v.VehicleType?.Name ?? string.Empty,
                Model = v.Model,
                Year = v.Year,
                Color = v.Color,
                LicensePlate = v.LicensePlate,
                CreatedAt = v.CreatedAt
            });
    }

    public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleRequest request)
    {
        if (await _vehicleRepository.GetByLicensePlateAsync(request.LicensePlate) != null)
        {
            throw new InvalidOperationException("License plate already exists");
        }

        var resident = await _residentRepository.GetByIdAsync(request.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId);
        if (vehicleType == null)
        {
            throw new InvalidOperationException("VehicleType not found");
        }

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            ResidentId = request.ResidentId,
            Brand = request.Brand,
            VehicleTypeId = request.VehicleTypeId,
            Model = request.Model,
            Year = request.Year,
            Color = request.Color,
            LicensePlate = request.LicensePlate,
            CreatedAt = DateTime.UtcNow
        };

        await _vehicleRepository.AddAsync(vehicle);

        return new VehicleDto
        {
            Id = vehicle.Id,
            ResidentId = vehicle.ResidentId,
            ResidentName = resident.FullName,
            Brand = vehicle.Brand,
            VehicleTypeId = vehicle.VehicleTypeId,
            VehicleTypeName = vehicleType.Name,
            Model = vehicle.Model,
            Year = vehicle.Year,
            Color = vehicle.Color,
            LicensePlate = vehicle.LicensePlate,
            CreatedAt = vehicle.CreatedAt
        };
    }

    public async Task<VehicleDto> UpdateVehicleAsync(Guid id, UpdateVehicleRequest request)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null)
        {
            throw new InvalidOperationException("Vehicle not found");
        }

        if (vehicle.LicensePlate != request.LicensePlate && await _vehicleRepository.GetByLicensePlateAsync(request.LicensePlate) != null)
        {
            throw new InvalidOperationException("License plate already exists");
        }

        var resident = await _residentRepository.GetByIdAsync(request.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId);
        if (vehicleType == null)
        {
            throw new InvalidOperationException("VehicleType not found");
        }

        vehicle.ResidentId = request.ResidentId;
        vehicle.Brand = request.Brand;
        vehicle.VehicleTypeId = request.VehicleTypeId;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        vehicle.Color = request.Color;
        vehicle.LicensePlate = request.LicensePlate;

        await _vehicleRepository.UpdateAsync(vehicle);

        return new VehicleDto
        {
            Id = vehicle.Id,
            ResidentId = vehicle.ResidentId,
            ResidentName = resident.FullName,
            Brand = vehicle.Brand,
            VehicleTypeId = vehicle.VehicleTypeId,
            VehicleTypeName = vehicleType.Name,
            Model = vehicle.Model,
            Year = vehicle.Year,
            Color = vehicle.Color,
            LicensePlate = vehicle.LicensePlate,
            CreatedAt = vehicle.CreatedAt
        };
    }

    public async Task<bool> DeleteVehicleAsync(Guid id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null)
        {
            return false;
        }

        await _vehicleRepository.DeleteAsync(vehicle);
        return true;
    }
}


