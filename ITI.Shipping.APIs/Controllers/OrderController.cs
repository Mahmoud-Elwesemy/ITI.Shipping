using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Shipping.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public OrderController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet] // Get : /api/Order
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrder()
    {
        var Orders = await _serviceManager.orderService.GetOrdersAsync();
        return Ok(Orders);
    }
    [HttpGet("{id}")] // Get : /api/Order/id
    public async Task<ActionResult<OrderDTO>> GetOrder(int id)
    {
        var Order = await _serviceManager.orderService.GetOrderAsync(id);
        if(Order == null)
            return NotFound();
        return Ok(Order);
    }
    [HttpPost] // Post : /api/Order
    public async Task<ActionResult<OrderDTO>> AddOrder(OrderDTO DTO)
    {
        if(DTO == null)
            return BadRequest("Invalid branch data");
        await _serviceManager.orderService.AddAsync(DTO);
        return Ok();
    }
    [HttpPut("{id}")] // Put : /api/Order/id
    public async Task<ActionResult> UpdateOrder(int id,[FromBody] OrderDTO DTO)
    {
        if(DTO == null || id != DTO.Id)
            return BadRequest("Invalid branch data.");
        try
        {
            await _serviceManager.orderService.UpdateAsync(DTO);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")] // Delete : /api/Order/id
    public async Task<ActionResult> DeleteOrder(int id)
    {
        try
        {
            await _serviceManager.orderService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
