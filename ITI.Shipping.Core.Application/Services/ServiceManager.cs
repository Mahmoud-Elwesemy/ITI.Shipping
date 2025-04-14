using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Branch;
using ITI.Shipping.Core.Application.Abstraction.CitySetting;
using ITI.Shipping.Core.Application.Abstraction.CourierReport;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.OrderReport;
using ITI.Shipping.Core.Application.Abstraction.Product;
using ITI.Shipping.Core.Application.Abstraction.Region;
using ITI.Shipping.Core.Application.Abstraction.ShippingType;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting;
using ITI.Shipping.Core.Application.Services.BranchServices;
using ITI.Shipping.Core.Application.Services.CitySettingServices;
using ITI.Shipping.Core.Application.Services.CourierReportServices;
using ITI.Shipping.Core.Application.Services.OrderReportServices;
using ITI.Shipping.Core.Application.Services.OrderServices;
using ITI.Shipping.Core.Application.Services.ProductServices;
using ITI.Shipping.Core.Application.Services.RegionServices;
using ITI.Shipping.Core.Application.Services.ShippingTypeServices;
using ITI.Shipping.Core.Application.Services.SpecialCityCostServices;
using ITI.Shipping.Core.Application.Services.SpecialCourierRegionServices;
using ITI.Shipping.Core.Application.Services.WeightSettingServices;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services
{
    public class ServiceManager :IServiceManager
    {
        private readonly Lazy<IBranchService> _branchService;
        private readonly Lazy<ICitySettingService> _citySettingService;
        private readonly Lazy<ICourierReportService> _courierReportService;
        private readonly Lazy<IRegionService> _regionService; 
        private readonly Lazy<ISpecialCourierRegionService>  _specialCourierRegionService;
        private readonly Lazy<ISpecialCityCostService> _SpecialCityCostService;
        private readonly Lazy<IShippingTypeService> _shippingTypeService;
        private readonly Lazy<IWeightSettingService> _weightSettingService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IProductService> _productService; 
        private readonly Lazy<IOrderReportService> _orderReportService;
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            _branchService = new Lazy<IBranchService>(() => new BranchService(unitOfWork,mapper));
            _citySettingService = new Lazy<ICitySettingService>(() => new CitySettingService(unitOfWork,mapper));
            _courierReportService = new Lazy<ICourierReportService>(() => new CourierReportService(unitOfWork,mapper));
            _regionService = new Lazy<IRegionService>(() => new RegionService(unitOfWork,mapper));
            _specialCourierRegionService = new Lazy<ISpecialCourierRegionService> (()=> new SpecialCourierRegionService(unitOfWork,mapper));
            _SpecialCityCostService = new Lazy<ISpecialCityCostService>(() => new SpecialCityCostService(unitOfWork,mapper));
            _shippingTypeService =new Lazy<IShippingTypeService> (() => new ShippingTypeService(unitOfWork,mapper));
            _weightSettingService = new Lazy<IWeightSettingService>(() => new WeightSettingService(unitOfWork,mapper));
            _orderService= new Lazy<IOrderService>(() => new OrderService(unitOfWork,mapper,userManager));
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork,mapper));
            _orderReportService = new Lazy<IOrderReportService>(()=> new OrderReportService(unitOfWork,mapper));
        }

        public IBranchService BranchService => _branchService.Value;

        public ICitySettingService CitySettingService => _citySettingService.Value;

        public ICourierReportService CourierReportService => _courierReportService.Value;

        public IRegionService RegionService => _regionService.Value;

        public ISpecialCourierRegionService SpecialCourierRegionService => _specialCourierRegionService.Value;

        public ISpecialCityCostService specialCityCostService => _SpecialCityCostService.Value;

        public IShippingTypeService shippingTypeService => _shippingTypeService.Value;

        public IWeightSettingService weightSettingService => _weightSettingService.Value;

        public IOrderService orderService => _orderService.Value;

        public IProductService productService =>_productService.Value;

        public IOrderReportService orderReportService => _orderReportService.Value;
    }
}
