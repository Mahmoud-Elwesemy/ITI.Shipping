using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
public class OrderRepository:GenericRepository<Order,int>, IOrderRepository
{
    private readonly ApplicationContext _Context;

    public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
        _Context = applicationContext;
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status)
    {
        var orders = _Context.Orders.Where(x => x.Status == status).ToListAsync();
        if(orders == null)
        {
            return null!;
        }
        return await orders;
    }
}
