using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Domin.UnitOfWork.Contract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        #region Try Using Lazy Way
        //public IGenericRepository<ApplicationUser , string> User { get; }
        //public IGenericRepository<CitySetting,int> City { get; }
        //public IGenericRepository<Region,int> Region { get; }
        //public IGenericRepository<Branch,int> Branch { get; }
        //public IGenericRepository<CourierReport,int> CourierReport { get; }
        //public IGenericRepository<Order,int> Order { get; }
        //public IGenericRepository<OrderReport,int> OrderReport { get; }
        //public IGenericRepository<Product,int> Product { get; }
        //public IGenericRepository<ShippingType,int> ShippingType { get; }
        //public IGenericRepository<WeightSetting,int> WeightSetting { get; } 
        #endregion
        IGenericRepository<T,Tkey> GetRepository<T, Tkey>()
            where T : class where Tkey : IEquatable<Tkey>;
        ICityRepository GetCityRepository();
        ISpecialCourierRegionRepository GetSpecialCourierRegionRepository();
        ISpecialCityCostRepository GetSpecialCityCostRepository();
        IOrderRepository GetOrderRepository();
        Task<int> CompleteAsync();
        IWeightSettingRepository GetWeightSettingRepository();
        IEmployeeRepository GetAllEmployeesAsync();
    }
}
