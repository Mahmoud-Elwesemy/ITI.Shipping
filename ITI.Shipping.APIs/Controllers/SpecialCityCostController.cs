using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Domin.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SpecialCityCostController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SpecialCityCostController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet] // Get : /api/SpecialCityCost
    public async Task<ActionResult<IEnumerable<SpecialCityCostDTO>>> GetAllSpecialCityCost()
    {
        var AllSpecialCityCost = await _serviceManager.specialCityCostService.GetAllSpecialCityCostAsync();
        return Ok(AllSpecialCityCost);
    }
    [HttpGet("{id}")] // Get : /api/SpecialCityCost/id
    public async Task <ActionResult<SpecialCityCostDTO>> GetSpecialCityCost(int id)
    {
        var SpecialCityCost = await _serviceManager.specialCityCostService.GetSpecialCityCostAsync(id);
        return Ok(SpecialCityCost);
    }
    [HttpPost] // Post : /api/SpecialCityCost
    public async Task<ActionResult<SpecialCityCostDTO>> AddSpecialCityCost(SpecialCityCostDTO DTO)
    {
        if(DTO == null)
            return BadRequest("Invalid SpecialCityCost data");
        await _serviceManager.specialCityCostService.AddAsync(DTO);
        return Ok();
    }
    [HttpPut("{id}")] // Put : /api/SpecialCityCost/id 
    public async Task<ActionResult> UpdateSpecialCityCost(int id,[FromBody] SpecialCityCostDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid branch data.");
        try
        {
            await _serviceManager.specialCityCostService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/SpecialCityCost/id
    public async Task<ActionResult> DeleteSpecialCityCost(int id)
    {
        try
        {
            await _serviceManager.specialCityCostService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
