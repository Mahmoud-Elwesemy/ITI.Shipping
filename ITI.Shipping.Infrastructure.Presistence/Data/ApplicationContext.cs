using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public virtual DbSet<SpecialCourierRegion> SpecialCourierRegion { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = passwordHasher.HashPassword(null!,DefaultUser.AdminPassword);
            builder.Entity<ApplicationUser>()
                .Property(a => a.CanceledOrder)
                .HasPrecision(18,2);
            builder.Entity<ApplicationUser>()
                .Property(a => a.DeductionCompanyFromOrder)
            .HasPrecision(18,2);
            builder.Entity<ApplicationUser>()
                .Property(a => a.PickupPrice)
            .HasPrecision(18,2);
            builder.Entity<CitySetting>()
                .Property(c => c.StandardShippingCost)
                .HasPrecision(18,2);
            builder.Entity<CitySetting>()
                .Property(c => c.pickupShippingCost)
                .HasPrecision(18,2);
            builder.Entity<Order>()
                .Property(o => o.OrderCost)
                .HasPrecision(18,2);
            builder.Entity<Order>()
                .Property(o => o.TotalWeight)
                .HasPrecision(18,2);
            builder.Entity<Product>()
                .Property(p => p.Weight)
                .HasPrecision(18,2);
            builder.Entity<ShippingType>()
                .Property(s => s.BaseCost)
                .HasPrecision(18,2);
            builder.Entity<WeightSetting>()
                .Property(w => w.CostPerKg)
                .HasPrecision(18,2);
            builder.Entity<WeightSetting>()
                .Property(w => w.MaxWeight)
                .HasPrecision(18,2);
            builder.Entity<WeightSetting>()
                .Property(w => w.MinWeight)
                .HasPrecision(18,2);
            builder.Entity<SpecialCityCost>()
                .Property(s => s.Price)
                .HasPrecision(18,2);
            builder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    Id = DefaultUser.AdminId,
                    FullName = "Weso Admin",
                    UserName = DefaultUser.AdminEmail,
                    NormalizedUserName = DefaultUser.AdminEmail.ToUpper(),
                    Email = DefaultUser.AdminEmail,
                    NormalizedEmail = DefaultUser.AdminEmail.ToUpper(),
                    SecurityStamp = DefaultUser.AdminSecurityStamp,
                    ConcurrencyStamp = DefaultUser.AdminConcurrencyStamp,
                    PasswordHash = hashedPassword
                });
            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = DefaultRole.AdminRoleId,
                    UserId = DefaultUser.AdminId
                });
            var permissions = Permissions.GetAllPermissions();
            var adminClaims = new List<IdentityRoleClaim<string>>();
            for(int i = 0;i < permissions.Count;i++)
            {
                adminClaims.Add(new IdentityRoleClaim<string>
                {
                    Id = i + 1,
                    RoleId = DefaultRole.AdminRoleId,
                    ClaimType = Permissions.Type,
                    ClaimValue = permissions[i]
                });
            }
            builder.Entity<IdentityRoleClaim<string>>()
                .HasData(adminClaims);
            base.OnModelCreating(builder);
        }
    }
}


