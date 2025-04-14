using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
public class OrderReportDTO
{
    public int Id { get; set; }
    public string ReportDetails { get; set; } = string.Empty;
    public DateTime ReportDate { get; set; } = DateTime.Now;
    //----------- Order  ---------------------------------
    public int? OrderId { get; set; }

}
