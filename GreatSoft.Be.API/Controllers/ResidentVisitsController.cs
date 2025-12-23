using GreatSoft.Be.Application.DTOs.ResidentVisit;
using GreatSoft.Be.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResidentVisitsController : ControllerBase
{
    private readonly IResidentVisitService _visitService;

    public ResidentVisitsController(IResidentVisitService visitService)
    {
        _visitService = visitService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentVisitDto>>> GetAllVisits()
    {
        var visits = await _visitService.GetAllVisitsAsync();
        return Ok(visits);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResidentVisitDto>> GetVisitById(Guid id)
    {
        var visit = await _visitService.GetVisitByIdAsync(id);
        if (visit == null)
        {
            return NotFound();
        }
        return Ok(visit);
    }

    [HttpGet("resident/{residentId}")]
    public async Task<ActionResult<IEnumerable<ResidentVisitDto>>> GetVisitsByResidentId(Guid residentId)
    {
        var visits = await _visitService.GetVisitsByResidentIdAsync(residentId);
        return Ok(visits);
    }

    [HttpPost]
    public async Task<ActionResult<ResidentVisitDto>> CreateVisit(CreateResidentVisitRequest request)
    {
        try
        {
            var visit = await _visitService.CreateVisitAsync(request);
            return CreatedAtAction(nameof(GetVisitById), new { id = visit.Id }, visit);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResidentVisitDto>> UpdateVisit(Guid id, UpdateResidentVisitRequest request)
    {
        try
        {
            var visit = await _visitService.UpdateVisitAsync(id, request);
            return Ok(visit);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVisit(Guid id)
    {
        var deleted = await _visitService.DeleteVisitAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}


