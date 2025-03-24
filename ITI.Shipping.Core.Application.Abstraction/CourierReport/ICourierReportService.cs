using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.CourierReport
{
    public interface ICourierReportService
    {

        Task<IEnumerable<GetAllCourierOrderCountDto>> GetAllCourierReportsAsync();
        Task<CourierReportDto> GetCourierReportByIdAsync(int id);
    }
}
