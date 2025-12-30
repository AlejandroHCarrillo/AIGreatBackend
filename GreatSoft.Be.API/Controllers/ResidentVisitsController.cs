using GreatSoft.Be.Application.DTOs.ResidentVisit;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResidentVisitsController : ControllerBase
{
    private readonly IResidentVisitService _residentVisitService;

    public ResidentVisitsController(IResidentVisitService residentVisitService)
    {
        _residentVisitService = residentVisitService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentVisitDto>>> GetAll()
    {
        var visits = await _residentVisitService.GetAllAsync();
        return Ok(visits);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResidentVisitDto>> GetById(int id)
    {
        var visit = await _residentVisitService.GetByIdAsync(id);
        if (visit == null)
        {
            return NotFound();
        }
        return Ok(visit);
    }

    [HttpPost]
    public async Task<ActionResult<ResidentVisitDto>> Create([FromBody] CreateResidentVisitDto createResidentVisitDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var visit = await _residentVisitService.CreateAsync(createResidentVisitDto);
            return CreatedAtAction(nameof(GetById), new { id = visit.Id }, visit);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<ResidentVisitDto>> Update(int id, [FromBody] UpdateResidentVisitDto updateResidentVisitDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var visit = await _residentVisitService.UpdateAsync(id, updateResidentVisitDto);
        if (visit == null)
        {
            return NotFound();
        }
        return Ok(visit);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _residentVisitService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

