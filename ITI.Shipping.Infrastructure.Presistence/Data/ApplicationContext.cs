using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Shipping.Core.Domin.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITI.Shipping.Infrastructure.Presistence.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options):IdentityDbContext<ApplicationUser , ApplicationRole , string>(options)
    {
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CitySetting> CitySettings { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderReport> OrderReports { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ShippingType> ShippingTypes { get; set; }
        public virtual DbSet<WeightSetting> WeightSettings { get; set; }
        public virtual DbSet<CourierReport> CourierReports { get; set; }
        public virtual DbSet<SpecialCityCost> SpecialCityCost { get; set; }
    }
}


