using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Product.Model;
public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }   
    public int Quantity { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
