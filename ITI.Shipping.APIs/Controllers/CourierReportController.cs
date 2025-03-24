using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierReportController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CourierReportController(IServiceManager serviceManager)
        {
           _serviceManager = serviceManager;
        }
        [HttpGet] // Get : /api/CourierReport
        public async Task<ActionResult<IEnumerable<GetAllCourierOrderCountDto>>> GetAllReports()
        {
            var CourierReports = await _serviceManager.CourierReportService.GetAllCourierReportsAsync();
            return Ok(CourierReports);
        }
        [HttpGet("{id}")] // Get : /api/CourierReport/id
        public async Task<ActionResult<CourierReportDto>> GetBranch(int id)
        {
            var CourierReport = await _serviceManager.CourierReportService.GetCourierReportByIdAsync(id);
            if(CourierReport == null)
            {
                return NotFound();
            }
            return Ok(CourierReport);
        }

    }
}
