using ITI.Shipping.Core.Application.Abstraction.Branch;
using ITI.Shipping.Core.Application.Abstraction.CitySetting;
using ITI.Shipping.Core.Application.Abstraction.CourierReport;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.Region;
using ITI.Shipping.Core.Application.Abstraction.ShippingType;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction
{
    public interface IServiceManager
    {       
        public IBranchService BranchService { get; }
        public ICitySettingService CitySettingService { get; }
        public ICourierReportService CourierReportService { get; }
        public IRegionService RegionService { get; }
        public ISpecialCourierRegionService SpecialCourierRegionService { get; }
        public ISpecialCityCostService specialCityCostService { get; }
        public IShippingTypeService shippingTypeService { get; }
        public IWeightSettingService weightSettingService { get; }
        public IOrderService orderService { get; }
    }
}
