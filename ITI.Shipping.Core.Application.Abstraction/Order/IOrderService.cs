using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderWithProductsDto>> GetOrdersAsync(Pramter pramter);
        Task<OrderWithProductsDto> GetOrderAsync(int id);
        Task<IEnumerable<OrderWithProductsDto>> GetOrdersByStatus(OrderStatus status);
        Task AddAsync(addOrderDto DTO,Pramter? pramter);
        Task UpdateAsync(updateOrderDto DTO);
        Task DeleteAsync(int id);
    }
}
