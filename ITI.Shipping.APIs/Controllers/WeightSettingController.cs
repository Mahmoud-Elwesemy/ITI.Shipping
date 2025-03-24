using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WeightSettingController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public WeightSettingController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet] // Get : /api/WeightSetting
    public async Task<ActionResult<IEnumerable<WeightSettingDTO>>> GetAllWeightSetting()
    {
        var allWeightSetting =await _serviceManager.weightSettingService.GetAllWeightSettingAsync();
        return Ok(allWeightSetting);
    }
    [HttpGet("{id}")] // Get : /api/WeightSetting/id
    public async Task<ActionResult<WeightSettingDTO>> GetWeightSetting(int id)
    {
        var WeightSetting = await _serviceManager.weightSettingService.GetWeightSettingAsync(id);
        return Ok(WeightSetting);
    }
    [HttpPost] // Post : /api/WeightSetting
    public async Task<ActionResult<WeightSettingDTO>> AddWeightSetting(WeightSettingDTO DTO)
    {
        if(DTO == null)
            return BadRequest("Invalid WeightSetting data");
        await _serviceManager.weightSettingService.AddAsync(DTO);
        return Ok();
    }
    [HttpPut("{id}")] // Put : /api/WeightSetting/id
    public async Task<ActionResult<WeightSettingDTO>> UpdateWeightSetting(int id,[FromBody] WeightSettingDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid SpecialCourierRegion data.");
        try
        {
            await _serviceManager.weightSettingService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/WeightSetting/id
    public async Task<ActionResult> DeleteWeightSetting(int id)
    {
        try
        {
            await _serviceManager.weightSettingService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}
