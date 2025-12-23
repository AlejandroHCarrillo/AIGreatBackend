using GreatSoft.Be.Application.DTOs.ResidentProvider;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;

namespace GreatSoft.Be.Application.Services;

public class ResidentProviderService : IResidentProviderService
{
    private readonly IResidentProviderRepository _providerRepository;
    private readonly IRepository<ProviderServiceType> _serviceTypeRepository;

    public ResidentProviderService(
        IResidentProviderRepository providerRepository,
        IRepository<ProviderServiceType> serviceTypeRepository)
    {
        _providerRepository = providerRepository;
        _serviceTypeRepository = serviceTypeRepository;
    }

    public async Task<IEnumerable<ResidentProviderDto>> GetAllProvidersAsync()
    {
        var providers = await _providerRepository.GetAllAsync();
        return providers.Select(p => new ResidentProviderDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            ProviderServiceTypeId = p.ProviderServiceTypeId,
            ProviderServiceTypeName = p.ProviderServiceType?.Name ?? string.Empty,
            Phone = p.Phone,
            Email = p.Email,
            Image = p.Image,
            CreatedAt = p.CreatedAt
        });
    }

    public async Task<ResidentProviderDto?> GetProviderByIdAsync(Guid id)
    {
        var provider = await _providerRepository.GetByIdAsync(id);
        if (provider == null) return null;

        return new ResidentProviderDto
        {
            Id = provider.Id,
            Name = provider.Name,
            Description = provider.Description,
            ProviderServiceTypeId = provider.ProviderServiceTypeId,
            ProviderServiceTypeName = provider.ProviderServiceType?.Name ?? string.Empty,
            Phone = provider.Phone,
            Email = provider.Email,
            Image = provider.Image,
            CreatedAt = provider.CreatedAt
        };
    }

    public async Task<IEnumerable<ResidentProviderDto>> GetProvidersByServiceTypeAsync(Guid serviceTypeId)
    {
        var providers = await _providerRepository.GetAllAsync();
        return providers
            .Where(p => p.ProviderServiceTypeId == serviceTypeId)
            .Select(p => new ResidentProviderDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ProviderServiceTypeId = p.ProviderServiceTypeId,
                ProviderServiceTypeName = p.ProviderServiceType?.Name ?? string.Empty,
                Phone = p.Phone,
                Email = p.Email,
                Image = p.Image,
                CreatedAt = p.CreatedAt
            });
    }

    public async Task<ResidentProviderDto> CreateProviderAsync(CreateResidentProviderRequest request)
    {
        if (await _providerRepository.GetByEmailAsync(request.Email) != null)
        {
            throw new InvalidOperationException("Provider email already exists");
        }

        var serviceType = await _serviceTypeRepository.GetByIdAsync(request.ProviderServiceTypeId);
        if (serviceType == null)
        {
            throw new InvalidOperationException("ProviderServiceType not found");
        }

        var provider = new ResidentProvider
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ProviderServiceTypeId = request.ProviderServiceTypeId,
            Phone = request.Phone,
            Email = request.Email,
            Image = request.Image,
            CreatedAt = DateTime.UtcNow
        };

        await _providerRepository.AddAsync(provider);

        return new ResidentProviderDto
        {
            Id = provider.Id,
            Name = provider.Name,
            Description = provider.Description,
            ProviderServiceTypeId = provider.ProviderServiceTypeId,
            ProviderServiceTypeName = serviceType.Name,
            Phone = provider.Phone,
            Email = provider.Email,
            Image = provider.Image,
            CreatedAt = provider.CreatedAt
        };
    }

    public async Task<ResidentProviderDto> UpdateProviderAsync(Guid id, UpdateResidentProviderRequest request)
    {
        var provider = await _providerRepository.GetByIdAsync(id);
        if (provider == null)
        {
            throw new InvalidOperationException("Provider not found");
        }

        if (provider.Email != request.Email && await _providerRepository.GetByEmailAsync(request.Email) != null)
        {
            throw new InvalidOperationException("Provider email already exists");
        }

        var serviceType = await _serviceTypeRepository.GetByIdAsync(request.ProviderServiceTypeId);
        if (serviceType == null)
        {
            throw new InvalidOperationException("ProviderServiceType not found");
        }

        provider.Name = request.Name;
        provider.Description = request.Description;
        provider.ProviderServiceTypeId = request.ProviderServiceTypeId;
        provider.Phone = request.Phone;
        provider.Email = request.Email;
        provider.Image = request.Image;

        await _providerRepository.UpdateAsync(provider);

        return new ResidentProviderDto
        {
            Id = provider.Id,
            Name = provider.Name,
            Description = provider.Description,
            ProviderServiceTypeId = provider.ProviderServiceTypeId,
            ProviderServiceTypeName = serviceType.Name,
            Phone = provider.Phone,
            Email = provider.Email,
            Image = provider.Image,
            CreatedAt = provider.CreatedAt
        };
    }

    public async Task<bool> DeleteProviderAsync(Guid id)
    {
        var provider = await _providerRepository.GetByIdAsync(id);
        if (provider == null)
        {
            return false;
        }

        await _providerRepository.DeleteAsync(provider);
        return true;
    }
}


