using GreatSoft.Be.Application.DTOs.Community;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommunitiesController : ControllerBase
{
    private readonly ICommunityService _communityService;

    public CommunitiesController(ICommunityService communityService)
    {
        _communityService = communityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommunityDto>>> GetAll()
    {
        var communities = await _communityService.GetAllAsync();
        return Ok(communities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommunityDto>> GetById(int id)
    {
        var community = await _communityService.GetByIdAsync(id);
        if (community == null)
        {
            return NotFound();
        }
        return Ok(community);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<CommunityDto>> Create([FromBody] CreateCommunityDto createCommunityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var community = await _communityService.CreateAsync(createCommunityDto);
            return CreatedAtAction(nameof(GetById), new { id = community.Id }, community);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult<CommunityDto>> Update(int id, [FromBody] UpdateCommunityDto updateCommunityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var community = await _communityService.UpdateAsync(id, updateCommunityDto);
        if (community == null)
        {
            return NotFound();
        }
        return Ok(community);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _communityService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

