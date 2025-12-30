using GreatSoft.Be.Application.DTOs.ResidentVisit;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class ResidentVisitService : IResidentVisitService
{
    private readonly IResidentVisitRepository _residentVisitRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserRepository _userRepository;

    public ResidentVisitService(
        IResidentVisitRepository residentVisitRepository,
        ICommunityRepository communityRepository,
        IUserRepository userRepository)
    {
        _residentVisitRepository = residentVisitRepository;
        _communityRepository = communityRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ResidentVisitDto>> GetAllAsync()
    {
        var visits = await _residentVisitRepository.GetAllAsync();
        return visits.Select(MapToDto);
    }

    public async Task<ResidentVisitDto?> GetByIdAsync(int id)
    {
        var visit = await _residentVisitRepository.GetByIdAsync(id);
        if (visit == null) return null;
        return MapToDto(visit);
    }

    public async Task<ResidentVisitDto> CreateAsync(CreateResidentVisitDto createResidentVisitDto)
    {
        // Verify community exists
        var community = await _communityRepository.GetByIdAsync(createResidentVisitDto.CommunityId);
        if (community == null)
        {
            throw new InvalidOperationException("Community not found");
        }

        // Verify resident exists
        var resident = await _userRepository.GetByIdAsync(createResidentVisitDto.ResidentId);
        if (resident == null)
        {
            throw new InvalidOperationException("Resident not found");
        }

        var visit = new ResidentVisit
        {
            VisitorName = createResidentVisitDto.VisitorName,
            VisitorDocument = createResidentVisitDto.VisitorDocument,
            VisitDate = createResidentVisitDto.VisitDate,
            VisitTime = createResidentVisitDto.VisitTime,
            CommunityId = createResidentVisitDto.CommunityId,
            ResidentId = createResidentVisitDto.ResidentId,
            Purpose = createResidentVisitDto.Purpose,
            Status = "Pending",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _residentVisitRepository.AddAsync(visit);
        var createdVisit = await _residentVisitRepository.GetByIdAsync(visit.Id);
        return MapToDto(createdVisit!);
    }

    public async Task<ResidentVisitDto?> UpdateAsync(int id, UpdateResidentVisitDto updateResidentVisitDto)
    {
        var visit = await _residentVisitRepository.GetByIdAsync(id);
        if (visit == null) return null;

        visit.VisitorName = updateResidentVisitDto.VisitorName;
        visit.VisitorDocument = updateResidentVisitDto.VisitorDocument;
        visit.VisitDate = updateResidentVisitDto.VisitDate;
        visit.VisitTime = updateResidentVisitDto.VisitTime;
        visit.Purpose = updateResidentVisitDto.Purpose;
        visit.Status = updateResidentVisitDto.Status;
        visit.IsActive = updateResidentVisitDto.IsActive;
        visit.UpdatedAt = DateTime.UtcNow;

        await _residentVisitRepository.UpdateAsync(visit);
        var updatedVisit = await _residentVisitRepository.GetByIdAsync(visit.Id);
        return MapToDto(updatedVisit!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var visit = await _residentVisitRepository.GetByIdAsync(id);
        if (visit == null) return false;

        await _residentVisitRepository.DeleteAsync(visit);
        return true;
    }

    private static ResidentVisitDto MapToDto(ResidentVisit visit)
    {
        return new ResidentVisitDto
        {
            Id = visit.Id,
            VisitorName = visit.VisitorName,
            VisitorDocument = visit.VisitorDocument,
            VisitDate = visit.VisitDate,
            VisitTime = visit.VisitTime,
            CommunityId = visit.CommunityId,
            CommunityName = visit.Community?.Name ?? string.Empty,
            ResidentId = visit.ResidentId,
            ResidentName = visit.Resident != null ? $"{visit.Resident.FirstName} {visit.Resident.LastName}" : string.Empty,
            Purpose = visit.Purpose,
            Status = visit.Status,
            IsActive = visit.IsActive
        };
    }
}

