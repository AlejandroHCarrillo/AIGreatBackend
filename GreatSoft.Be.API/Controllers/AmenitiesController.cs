using GreatSoft.Be.Application.DTOs.Amenity;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class AmenitiesController : ControllerBase
{
    private readonly IAmenityService _amenityService;

    public AmenitiesController(IAmenityService amenityService)
    {
        _amenityService = amenityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AmenityDto>>> GetAll()
    {
        var amenities = await _amenityService.GetAllAsync();
        return Ok(amenities);
    }

    [HttpGet("community/{communityId}")]
    public async Task<ActionResult<IEnumerable<AmenityDto>>> GetByCommunityId(int communityId)
    {
        var amenities = await _amenityService.GetByCommunityIdAsync(communityId);
        return Ok(amenities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AmenityDto>> GetById(int id)
    {
        var amenity = await _amenityService.GetByIdAsync(id);
        if (amenity == null)
        {
            return NotFound();
        }
        return Ok(amenity);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<AmenityDto>> Create([FromBody] CreateAmenityDto createAmenityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var amenity = await _amenityService.CreateAsync(createAmenityDto);
            return CreatedAtAction(nameof(GetById), new { id = amenity.Id }, amenity);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<AmenityDto>> Update(int id, [FromBody] UpdateAmenityDto updateAmenityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var amenity = await _amenityService.UpdateAsync(id, updateAmenityDto);
        if (amenity == null)
        {
            return NotFound();
        }
        return Ok(amenity);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _amenityService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

