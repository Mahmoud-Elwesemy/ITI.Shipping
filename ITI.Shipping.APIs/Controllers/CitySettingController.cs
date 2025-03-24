using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitySettingController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CitySettingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet] // Get : /api/CitySetting
        public async Task<ActionResult<IEnumerable<CitySettingDTO>>> GetCitySettings()
        {
            var CitySetting = await _serviceManager.CitySettingService.GetCitySettingsAsync();
            return Ok(CitySetting);
        }
        [HttpGet("{id}")] // Get : /api/CitySetting/id
        public async Task<ActionResult<CitySettingDTO>> GetCitySetting(int id)
        {
            var CitySetting = await _serviceManager.CitySettingService.GetCitySettingAsync(id);
            if(CitySetting == null)
            {
                return NotFound();
            }
            return Ok(CitySetting);
        }
        [HttpPost] // Post : /api/CitySetting
        public async Task<ActionResult<CitySettingToAddDTO>> AddCitySetting(CitySettingToAddDTO DTO)
        {
            if(DTO == null)
                return BadRequest("Invalid CitySetting data");
            await _serviceManager.CitySettingService.AddAsync(DTO);
            return Ok();
        }
        [HttpPut("{id}")] // Put : /api/CitySetting/id
        public async Task<ActionResult> UpdateCitySetting(int id,[FromBody] CitySettingToUpdateDTO DTO)
        {
            if(DTO == null || id != DTO.Id)
                return BadRequest("Invalid CitySetting data.");
            try
            {
                await _serviceManager.CitySettingService.UpdateAsync(DTO);
                return NoContent(); // 204 No Content (successful update)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")] // Delete : /api/CitySetting/id
        public async Task<ActionResult> DeleteCitySetting(int id)
        {
            try
            {
                await _serviceManager.CitySettingService.DeleteAsync(id);
                return NoContent(); // 204 No Content (successful deletion)
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
