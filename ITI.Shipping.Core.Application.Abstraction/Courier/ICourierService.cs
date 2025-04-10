using ITI.Shipping.Core.Application.Abstraction.Courier.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Courier;
public interface ICourierService
{
    Task <IEnumerable<CourierDTO>> GetCourierByBranch(int OrderId);
}

