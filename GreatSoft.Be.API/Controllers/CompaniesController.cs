using GreatSoft.Be.Application.DTOs.Company;
using GreatSoft.Be.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreatSoft.Be.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ILogger<CompaniesController> _logger;

    public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompanies()
    {
        try
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all companies");
            return StatusCode(500, new { message = "An error occurred while retrieving companies" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id)
    {
        try
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound(new { message = "Company not found" });
            }
            return Ok(company);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting company by id: {Id}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the company" });
        }
    }

    [HttpPost]
    [Authorize(Roles = "SysAdmin")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CreateCompanyRequest request)
    {
        try
        {
            var company = await _companyService.CreateCompanyAsync(request);
            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error creating company");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating company");
            return StatusCode(500, new { message = "An error occurred while creating the company" });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> UpdateCompany(Guid id, [FromBody] UpdateCompanyRequest request)
    {
        try
        {
            var company = await _companyService.UpdateCompanyAsync(id, request);
            return Ok(company);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error updating company: {Id}", id);
            if (ex.Message.Contains("not found"))
            {
                return NotFound(new { message = ex.Message });
            }
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating company: {Id}", id);
            return StatusCode(500, new { message = "An error occurred while updating the company" });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCompany(Guid id)
    {
        try
        {
            var deleted = await _companyService.DeleteCompanyAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Company not found" });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting company: {Id}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the company" });
        }
    }
}


