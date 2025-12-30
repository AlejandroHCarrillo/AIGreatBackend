using GreatSoft.Be.Application.DTOs.ResidentProvider;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResidentProvidersController : ControllerBase
{
    private readonly IResidentProviderService _residentProviderService;

    public ResidentProvidersController(IResidentProviderService residentProviderService)
    {
        _residentProviderService = residentProviderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentProviderDto>>> GetAll()
    {
        var providers = await _residentProviderService.GetAllAsync();
        return Ok(providers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResidentProviderDto>> GetById(int id)
    {
        var provider = await _residentProviderService.GetByIdAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        return Ok(provider);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<ResidentProviderDto>> Create([FromBody] CreateResidentProviderDto createResidentProviderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var provider = await _residentProviderService.CreateAsync(createResidentProviderDto);
            return CreatedAtAction(nameof(GetById), new { id = provider.Id }, provider);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<ResidentProviderDto>> Update(int id, [FromBody] UpdateResidentProviderDto updateResidentProviderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var provider = await _residentProviderService.UpdateAsync(id, updateResidentProviderDto);
        if (provider == null)
        {
            return NotFound();
        }
        return Ok(provider);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _residentProviderService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

