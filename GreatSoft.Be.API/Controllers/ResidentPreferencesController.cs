using GreatSoft.Be.Application.DTOs.ResidentPreference;
using GreatSoft.Be.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResidentPreferencesController : ControllerBase
{
    private readonly IResidentPreferenceService _residentPreferenceService;

    public ResidentPreferencesController(IResidentPreferenceService residentPreferenceService)
    {
        _residentPreferenceService = residentPreferenceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentPreferenceDto>>> GetAll()
    {
        var preferences = await _residentPreferenceService.GetAllAsync();
        return Ok(preferences);
    }

    [HttpGet("resident/{residentId}")]
    public async Task<ActionResult<IEnumerable<ResidentPreferenceDto>>> GetByResidentId(int residentId)
    {
        var preferences = await _residentPreferenceService.GetByResidentIdAsync(residentId);
        return Ok(preferences);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResidentPreferenceDto>> GetById(int id)
    {
        var preference = await _residentPreferenceService.GetByIdAsync(id);
        if (preference == null)
        {
            return NotFound();
        }
        return Ok(preference);
    }

    [HttpPost]
    public async Task<ActionResult<ResidentPreferenceDto>> Create([FromBody] CreateResidentPreferenceDto createResidentPreferenceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var preference = await _residentPreferenceService.CreateAsync(createResidentPreferenceDto);
            return CreatedAtAction(nameof(GetById), new { id = preference.Id }, preference);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResidentPreferenceDto>> Update(int id, [FromBody] UpdateResidentPreferenceDto updateResidentPreferenceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var preference = await _residentPreferenceService.UpdateAsync(id, updateResidentPreferenceDto);
        if (preference == null)
        {
            return NotFound();
        }
        return Ok(preference);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _residentPreferenceService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}


