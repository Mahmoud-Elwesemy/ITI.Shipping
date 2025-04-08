using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Domin.Repositories.contract;
public interface IOrderRepository:IGenericRepository<Order,int>
{
    Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status);
}
