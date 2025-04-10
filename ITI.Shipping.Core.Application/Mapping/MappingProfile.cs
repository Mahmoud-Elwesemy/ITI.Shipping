using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Application.Abstraction.Courier.DTO;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Application.Abstraction.User.Model;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            #region Configratio Of Branch
            CreateMap<Branch,BranchDTO>()
             .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region != null ? src.Region.Id : (int?) null))
             .ForMember(dest => dest.RegionName,opt => opt.MapFrom(src => src.Region != null ? src.Region.Governorate : null))
             .ForMember(dest => dest.UsersName,opt => opt.MapFrom(src => src.Users.Select(u => u.FullName).ToList()))
             .ReverseMap();
            CreateMap<BranchToAddDTO,Branch>().ReverseMap();
            CreateMap<BranchToUpdateDTO,Branch>().ReverseMap();
            #endregion
            #region Configratio Of CitySetting
            CreateMap<CitySetting,CitySettingDTO>()
             .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region != null ? src.Region.Id : (int?) null))
             .ForMember(dest => dest.RegionName,opt => opt.MapFrom(src => src.Region != null ? src.Region.Governorate : null))
             .ForMember(dest => dest.UsersName,opt => opt.MapFrom(src => src.Users.Select(u => u.FullName).ToList()))
             .ForMember(dest => dest.OrdersCost,opt => opt.MapFrom(src => src.Orders.Select(u => u.OrderCost).ToList()))
             .ForMember(dest => dest.UsersThatHasSpecialCityCost,opt => opt.MapFrom(src => src.SpecialPickups.Select(u => u.Merchant.FullName).ToList()))
             .ReverseMap();
            CreateMap<CitySettingToAddDTO,CitySetting>().ReverseMap();
            CreateMap<CitySettingToUpdateDTO,CitySetting>().ReverseMap();
            #endregion
            #region  Configratio Of CourierReport
            CreateMap<CourierReport,CourierReportDto>()
                 .ForMember(dest => dest.CourierName,opt => opt.MapFrom(src => src.Courier != null ? src.Courier.FullName : string.Empty))
                 .ForMember(dest => dest.Area,opt => opt.MapFrom(src => src.Order != null && src.Order.CitySetting != null ? src.Order.CitySetting.Name : string.Empty))
                 .ForMember(dest => dest.ClientName,opt => opt.MapFrom(src => src.Order != null ? src.Order.MerchantId : string.Empty))
                 .ForMember(dest => dest.CustomerName,opt => opt.MapFrom(src => src.Order != null ? src.Order.CustomerName : string.Empty))
                 .ForMember(dest => dest.CustomerPhone,opt => opt.MapFrom(src => src.Order != null ? src.Order.CustomerPhone1 : string.Empty))
                 .ForMember(dest => dest.CustomerAddress,opt => opt.MapFrom(src => src.Order != null ? src.Order.CustomerAddress : string.Empty))
                 .ForMember(dest => dest.products,opt => opt.MapFrom(src => src.Order != null && src.Order.Products != null
                    ? src.Order.Products.Select(x => x.Name).ToList()
                    : new List<string>()))
                 .ForMember(dest => dest.orderStatus,opt => opt.MapFrom(src => src.Order != null
                 ? src.Order.Status.ToString() 
                 : string.Empty))
                 .ForMember(dest => dest.Amount,opt => opt.MapFrom(src => src.Order != null ? src.Order.OrderCost : 0))
                 .ForMember(dest => dest.Notes,opt => opt.MapFrom(src => src.Order != null ? src.Order.Notes : string.Empty))
                 .ReverseMap();

            CreateMap<Region,RegionDto>()
                .ReverseMap();
            #endregion
            #region Configratio Of Region
            CreateMap<RegionDto,Region>().ReverseMap();
            #endregion
            #region  Configratio Of SpecialCourierRegion
            CreateMap<SpecialCourierRegion,SpecialCourierRegionDTO>()
                 .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region != null ? src.Region.Id : (int?) null))
                 .ForMember(dest => dest.RegionName,opt => opt.MapFrom(src => src.Region != null ? src.Region.Governorate : null))
                 .ForMember(dest => dest.CourierId,opt => opt.MapFrom(src => src.Courier != null ? src.Courier.Id : null))
                 .ForMember(dest => dest.CourierName,opt => opt.MapFrom(src => src.Courier != null ? src.Courier.FullName : null))
                 .ReverseMap();
            #endregion
            #region Configratio Of SpecialCityCost
            CreateMap<SpecialCityCost,SpecialCityCostDTO>()
               .ForMember(dest => dest.MerchantId,op => op.MapFrom(src => src.Merchant != null ? src.Merchant.Id : null))
               .ForMember(des => des.MerchantName,op => op.MapFrom(src => src.Merchant != null ? src.Merchant.FullName :  null))
               .ForMember(des => des.CitySettingId,op => op.MapFrom(src => src.CitySetting != null ? src.CitySetting.Id : (int?) null))
               .ForMember(des => des.CitySettingName,op => op.MapFrom(src => src.CitySetting != null ? src.CitySetting.Name : null))
               .ReverseMap();
            #endregion
            #region Configratio Of ShippingType
            CreateMap<ShippingType,ShippingTypeDTO>()
                   .ForMember(des => des.OrdersId,opt => opt.MapFrom(src => src.Branches.Select(x => x.Id).ToList()))
                   .ReverseMap();
            #endregion
            #region Configratio Of WeightSetting
            CreateMap<WeightSetting,WeightSettingDTO>().ReverseMap();
            #endregion
            #region  Configratio Of Product
            //CreateMap<Product,ProductDTO>()
            //    .ForMember(dest => dest.OrderId,opt => opt.MapFrom(src => src.OrderId))
            //    .ReverseMap();

            CreateMap<Product,ProductDTO>().ReverseMap();
            #endregion
            #region Configratio Of Order
            CreateMap<Order,OrderWithProductsDto>().AfterMap((src,dest) =>
            {

                dest.Branch = src.Branch?.Name;
                dest.Region = src.Region?.Governorate;
                dest.City = src.CitySetting?.Name;
                dest.MerchantName = src.MerchantId;
                dest.Status = src.Status.ToString();
                dest.CustomerInfo = $"{src.CustomerName} {src.CustomerPhone1}";

            }).ReverseMap();

            CreateMap<Order,updateOrderDto>().ReverseMap().ForMember(dest => dest.CitySettingId,opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.BranchId,opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.ShippingTypeId,opt => opt.MapFrom(src => src.ShippingId))
            .ForMember(dest => dest.PaymentType,opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.MerchantId,opt => opt.MapFrom(src => src.MerchantName))
            .ForMember(dest => dest.Branch,opt => opt.Ignore())
            .ForMember(dest => dest.Region,opt => opt.Ignore())
            .ForMember(dest => dest.ShippingType,opt => opt.Ignore())
            .ForMember(dest => dest.CitySetting,opt => opt.Ignore());

            CreateMap<Order,addOrderDto>().ReverseMap().ForMember(dest => dest.CitySettingId,opt => opt.MapFrom(src => src.City))
             .ForMember(dest => dest.BranchId,opt => opt.MapFrom(src => src.Branch))
             .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region))
             .ForMember(dest => dest.ShippingTypeId,opt => opt.MapFrom(src => src.ShippingId))
             .ForMember(dest => dest.PaymentType,opt => opt.MapFrom(src => src.PaymentType))
             .ForMember(dest => dest.MerchantId,opt => opt.MapFrom(src => src.MerchantName))
             .ForMember(dest => dest.Branch,opt => opt.Ignore())
             .ForMember(dest => dest.Region,opt => opt.Ignore())
             .ForMember(dest => dest.ShippingType,opt => opt.Ignore())
             .ForMember(dest => dest.CitySetting,opt => opt.Ignore());
            #endregion
            #region  Configratio Of OrderReport
            CreateMap<OrderReport,OrderReportDTO>()
            .ForMember(dest => dest.OrderId,op => op.MapFrom(src => src.OrderId))
            .ReverseMap();
            #endregion

            CreateMap<ApplicationUser,CourierDTO>()
                .ForMember(dest => dest.CourierId,opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CourierName,opt => opt.MapFrom(src => src.FullName))
                .ReverseMap();
                


            CreateMap<AddEmployeeDTO,ApplicationUser>().AfterMap((src,dest) =>
            {
                dest.UserName = src.Email;
            });
            CreateMap<AddMerchantDTO,ApplicationUser>().AfterMap((src,dest) =>
            {
                dest.UserName = src.Email;
            });
            CreateMap<AddCourierDTO,ApplicationUser>().AfterMap((src,dest) =>
            {
                dest.UserName = src.Email;
            });
            CreateMap<SpecialCityCostDT0,SpecialCityCost>().ReverseMap();
            CreateMap<CourierRegionDT0,SpecialCourierRegion>().ReverseMap();
            CreateMap<SpecialCourierRegionDTO,SpecialCourierRegion>().ReverseMap();
        }
    }
}
