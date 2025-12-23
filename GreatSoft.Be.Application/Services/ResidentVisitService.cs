using GreatSoft.Be.Application.DTOs.ResidentVisit;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class ResidentVisitService : IResidentVisitService
{
    private readonly IResidentVisitRepository _visitRepository;
    private readonly IRepository<Resident> _residentRepository;

    public ResidentVisitService(
        IResidentVisitRepository visitRepository,
        IRepository<Resident> residentRepository)
    {
        _visitRepository = visitRepository;
        _residentRepository = residentRepository;
    }

    public async Task<IEnumerable<ResidentVisitDto>> GetAllVisitsAsync()
    {
        var visits = await _visitRepository.GetAllAsync();
        return visits.Select(v => new ResidentVisitDto
        {
            Id = v.Id,
            ResidentId = v.ResidentId,
            ResidentName = v.Resident?.FullName ?? string.Empty,
            VisitorName = v.VisitorName,
            TotalPeople = v.TotalPeople,
            VehicleColor = v.VehicleColor,
            LicensePlate = v.LicensePlate,
            Subject = v.Subject,
            ArrivalDate = v.ArrivalDate,
            DepartureDate = v.DepartureDate,
            CreatedAt = v.CreatedAt
        });
    }

    public async Task<ResidentVisitDto?> GetVisitByIdAsync(Guid id)
    {
        var visit = await _visitRepository.GetByIdAsync(id);
        if (visit == null) return null;

        return new ResidentVisitDto
        {
            Id = visit.Id,
            ResidentId = visit.ResidentId,
            ResidentName = visit.Resident?.FullName ?? string.Empty,
            VisitorName = visit.VisitorName,
            TotalPeople = visit.TotalPeople,
            VehicleColor = visit.VehicleColor,
            LicensePlate = visit.LicensePlate,
            Subject = visit.Subject,
            ArrivalDate = visit.ArrivalDate,
            DepartureDate = visit.DepartureDate,
            CreatedAt = visit.CreatedAt
        };
    }

    public async Task<IEnumerable<ResidentVisitDto>> GetVisitsByResidentIdAsync(Guid residentId)
    {
        var visits = await _visitRepository.GetAllAsync();
        return visits
            .Where(v => v.ResidentId == residentId)
            .Select(v => new ResidentVisitDto
            {
                Id = v.Id,
                ResidentId = v.ResidentId,
                ResidentName = v.Resident?.FullName ?? string.Empty,
                VisitorName = v.VisitorName,
                TotalPeople = v.TotalPeople,
                VehicleColor = v.VehicleColor,
                LicensePlate = v.LicensePlate,
                Subject = v.Subject,
                ArrivalDate = v.ArrivalDate,
                DepartureDate = v.DepartureDate,
                CreatedAt = v.CreatedAt
            });
    }

    public async Task<ResidentVisitDto> CreateVisitAsync(CreateResidentVisitRequest request)
    {
        var resident = await _residentRepository.GetByIdAsync(request.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        var visit = new ResidentVisit
        {
            Id = Guid.NewGuid(),
            ResidentId = request.ResidentId,
            VisitorName = request.VisitorName,
            TotalPeople = request.TotalPeople,
            VehicleColor = request.VehicleColor,
            LicensePlate = request.LicensePlate,
            Subject = request.Subject,
            ArrivalDate = request.ArrivalDate,
            DepartureDate = request.DepartureDate,
            CreatedAt = DateTime.UtcNow
        };

        await _visitRepository.AddAsync(visit);

        return new ResidentVisitDto
        {
            Id = visit.Id,
            ResidentId = visit.ResidentId,
            ResidentName = resident.FullName,
            VisitorName = visit.VisitorName,
            TotalPeople = visit.TotalPeople,
            VehicleColor = visit.VehicleColor,
            LicensePlate = visit.LicensePlate,
            Subject = visit.Subject,
            ArrivalDate = visit.ArrivalDate,
            DepartureDate = visit.DepartureDate,
            CreatedAt = visit.CreatedAt
        };
    }

    public async Task<ResidentVisitDto> UpdateVisitAsync(Guid id, UpdateResidentVisitRequest request)
    {
        var visit = await _visitRepository.GetByIdAsync(id);
        if (visit == null)
        {
            throw new InvalidOperationException("Visit not found");
        }

        var resident = await _residentRepository.GetByIdAsync(request.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        visit.ResidentId = request.ResidentId;
        visit.VisitorName = request.VisitorName;
        visit.TotalPeople = request.TotalPeople;
        visit.VehicleColor = request.VehicleColor;
        visit.LicensePlate = request.LicensePlate;
        visit.Subject = request.Subject;
        visit.ArrivalDate = request.ArrivalDate;
        visit.DepartureDate = request.DepartureDate;

        await _visitRepository.UpdateAsync(visit);

        return new ResidentVisitDto
        {
            Id = visit.Id,
            ResidentId = visit.ResidentId,
            ResidentName = resident.FullName,
            VisitorName = visit.VisitorName,
            TotalPeople = visit.TotalPeople,
            VehicleColor = visit.VehicleColor,
            LicensePlate = visit.LicensePlate,
            Subject = visit.Subject,
            ArrivalDate = visit.ArrivalDate,
            DepartureDate = visit.DepartureDate,
            CreatedAt = visit.CreatedAt
        };
    }

    public async Task<bool> DeleteVisitAsync(Guid id)
    {
        var visit = await _visitRepository.GetByIdAsync(id);
        if (visit == null)
        {
            return false;
        }

        await _visitRepository.DeleteAsync(visit);
        return true;
    }
}


