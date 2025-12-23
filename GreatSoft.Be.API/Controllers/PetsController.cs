using GreatSoft.Be.Application.DTOs.Pet;
using GreatSoft.Be.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetAllPets()
    {
        var pets = await _petService.GetAllPetsAsync();
        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetDto>> GetPetById(Guid id)
    {
        var pet = await _petService.GetPetByIdAsync(id);
        if (pet == null)
        {
            return NotFound();
        }
        return Ok(pet);
    }

    [HttpGet("resident/{residentId}")]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetPetsByResidentId(Guid residentId)
    {
        var pets = await _petService.GetPetsByResidentIdAsync(residentId);
        return Ok(pets);
    }

    [HttpPost]
    public async Task<ActionResult<PetDto>> CreatePet(CreatePetRequest request)
    {
        try
        {
            var pet = await _petService.CreatePetAsync(request);
            return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, pet);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PetDto>> UpdatePet(Guid id, UpdatePetRequest request)
    {
        try
        {
            var pet = await _petService.UpdatePetAsync(id, request);
            return Ok(pet);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(Guid id)
    {
        var deleted = await _petService.DeletePetAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}


