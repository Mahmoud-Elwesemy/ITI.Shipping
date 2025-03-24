using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;

namespace ITI.Shipping.Core.Application.Abstraction.Order.Model
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal TotalWeight { get; set; }

        [Range(0.01,double.MaxValue,ErrorMessage = "Order cost must be greater than zero")]
        public decimal OrderCost { get; set; }
        public string? Notes { get; set; }
        public bool IsOutOfCityShipping { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Ids From User
        public int? BranchId { get; set; }
        public int? RegionId { get; set; }
        public int? CityId { get; set; }
        public string? BranchName { get; set; }
        public string? RegionName { get; set; }
        //customer info
        public string? PaymentTypeName { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? CityName { get; set; }

        //customer info

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone1 { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        // merchant 

        public string MerchantId { get; set; } = string.Empty;
        public string MerchantName { get; set; } = string.Empty;


        //public string CourierId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        //list of products
        //[MinLength(1,ErrorMessage = "At least one product is required")]
        public List<ProductDTO> Products { get; set; } = new();

        // ShippingType
        public int? ShippingTypeId { get; set; }
        public string ShippingTypeName { get; set; } = string.Empty;
        public decimal ShippingTypeBaseCost { get; set; }
        public int ShippingTypeDuration { get; set; }
    }
}
