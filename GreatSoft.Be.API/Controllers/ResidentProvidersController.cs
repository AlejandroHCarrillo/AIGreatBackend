using GreatSoft.Be.Application.DTOs.ResidentProvider;
using GreatSoft.Be.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResidentProvidersController : ControllerBase
{
    private readonly IResidentProviderService _providerService;

    public ResidentProvidersController(IResidentProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentProviderDto>>> GetAllProviders()
    {
        var providers = await _providerService.GetAllProvidersAsync();
        return Ok(providers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResidentProviderDto>> GetProviderById(Guid id)
    {
        var provider = await _providerService.GetProviderByIdAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        return Ok(provider);
    }

    [HttpGet("service-type/{serviceTypeId}")]
    public async Task<ActionResult<IEnumerable<ResidentProviderDto>>> GetProvidersByServiceType(Guid serviceTypeId)
    {
        var providers = await _providerService.GetProvidersByServiceTypeAsync(serviceTypeId);
        return Ok(providers);
    }

    [HttpPost]
    public async Task<ActionResult<ResidentProviderDto>> CreateProvider(CreateResidentProviderRequest request)
    {
        try
        {
            var provider = await _providerService.CreateProviderAsync(request);
            return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResidentProviderDto>> UpdateProvider(Guid id, UpdateResidentProviderRequest request)
    {
        try
        {
            var provider = await _providerService.UpdateProviderAsync(id, request);
            return Ok(provider);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProvider(Guid id)
    {
        var deleted = await _providerService.DeleteProviderAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}


