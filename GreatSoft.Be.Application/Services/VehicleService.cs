using GreatSoft.Be.Application.DTOs.Vehicle;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserRepository _userRepository;

    public VehicleService(
        IVehicleRepository vehicleRepository,
        ICommunityRepository communityRepository,
        IUserRepository userRepository)
    {
        _vehicleRepository = vehicleRepository;
        _communityRepository = communityRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles.Select(MapToDto);
    }

    public async Task<VehicleDto?> GetByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return null;
        return MapToDto(vehicle);
    }

    public async Task<VehicleDto> CreateAsync(CreateVehicleDto createVehicleDto)
    {
        // Check if license plate already exists
        var existingVehicle = await _vehicleRepository.GetByLicensePlateAsync(createVehicleDto.LicensePlate);
        if (existingVehicle != null)
        {
            throw new InvalidOperationException("License plate already exists");
        }

        // Verify community exists
        var community = await _communityRepository.GetByIdAsync(createVehicleDto.CommunityId);
        if (community == null)
        {
            throw new InvalidOperationException("Community not found");
        }

        // Verify owner exists
        var owner = await _userRepository.GetByIdAsync(createVehicleDto.OwnerId);
        if (owner == null)
        {
            throw new InvalidOperationException("Owner not found");
        }

        var vehicle = new Vehicle
        {
            LicensePlate = createVehicleDto.LicensePlate,
            Brand = createVehicleDto.Brand,
            Model = createVehicleDto.Model,
            Color = createVehicleDto.Color,
            Year = createVehicleDto.Year,
            CommunityId = createVehicleDto.CommunityId,
            OwnerId = createVehicleDto.OwnerId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _vehicleRepository.AddAsync(vehicle);
        var createdVehicle = await _vehicleRepository.GetByIdAsync(vehicle.Id);
        return MapToDto(createdVehicle!);
    }

    public async Task<VehicleDto?> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return null;

        vehicle.Brand = updateVehicleDto.Brand;
        vehicle.Model = updateVehicleDto.Model;
        vehicle.Color = updateVehicleDto.Color;
        vehicle.Year = updateVehicleDto.Year;
        vehicle.IsActive = updateVehicleDto.IsActive;
        vehicle.UpdatedAt = DateTime.UtcNow;

        await _vehicleRepository.UpdateAsync(vehicle);
        var updatedVehicle = await _vehicleRepository.GetByIdAsync(vehicle.Id);
        return MapToDto(updatedVehicle!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return false;

        await _vehicleRepository.DeleteAsync(vehicle);
        return true;
    }

    private static VehicleDto MapToDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Color = vehicle.Color,
            Year = vehicle.Year,
            CommunityId = vehicle.CommunityId,
            CommunityName = vehicle.Community?.Name ?? string.Empty,
            OwnerId = vehicle.OwnerId,
            OwnerName = vehicle.Owner != null ? $"{vehicle.Owner.FirstName} {vehicle.Owner.LastName}" : string.Empty,
            IsActive = vehicle.IsActive
        };
    }
}

