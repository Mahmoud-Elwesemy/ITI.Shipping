using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
public class EmployeeRepository:GenericRepository<ApplicationUser,string>, IEmployeeRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public EmployeeRepository(ApplicationContext _Context,UserManager<ApplicationUser> userManager) : base(_Context)
    {
        _userManager = userManager;
    }
    public async Task<IEnumerable<ApplicationUser>> GetAllEmployeesAsync(Pramter pramter)
    {
 

        if(pramter.PageSize != null && pramter.PageNumber != null)
        {
            var allUsers = await _userManager.GetUsersInRoleAsync(DefaultRole.Merchant);
            return allUsers
                .Skip((pramter.PageNumber.Value - 1) * pramter.PageSize.Value)
                .Take(pramter.PageSize.Value);
        }
        else
        {
            return await _userManager.GetUsersInRoleAsync(DefaultRole.Merchant);
        }
    }
}
