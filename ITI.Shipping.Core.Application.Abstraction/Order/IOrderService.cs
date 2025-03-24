using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<OrderDTO> GetOrderAsync(int id);
        Task AddAsync(OrderDTO DTO);
        Task UpdateAsync(OrderDTO DTO);
        Task DeleteAsync(int id);
    }
}
