using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Application.Abstraction.CourierReport.Model;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using System;
using System.Collections.Generic;
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
            CreateMap<Product,ProductDTO>().ReverseMap();
            #endregion
            #region Configratio Of Order
            CreateMap<Order,OrderDTO>()
                      // Basic Properties
                      .ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id))
                      .ForMember(dest => dest.TotalWeight,opt => opt.MapFrom(src => src.TotalWeight))
                      .ForMember(dest => dest.OrderCost,opt => opt.MapFrom(src => src.OrderCost))
                      .ForMember(dest => dest.Notes,opt => opt.MapFrom(src => src.Notes))
                      .ForMember(dest => dest.CreatedAt,opt => opt.MapFrom(src => src.CreatedAt))
                      .ForMember(dest => dest.IsOutOfCityShipping,opt => opt.MapFrom(src => src.IsOutOfCityShipping))

                      // Foreign Keys & Related Entities
                      .ForMember(dest => dest.CityId,opt => opt.MapFrom(src => src.CitySettingId))
                      .ForMember(dest => dest.CityName,opt => opt.MapFrom(src => src.CitySetting != null ? src.CitySetting.Name : string.Empty))
                      .ForMember(dest => dest.BranchId,opt => opt.MapFrom(src => src.BranchId))
                      .ForMember(dest => dest.BranchName,opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : string.Empty))
                      .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.RegionId))
                      .ForMember(dest => dest.RegionName,opt => opt.MapFrom(src => src.Region != null ? src.Region.Governorate : string.Empty))

                      // Shipping Type
                      .ForMember(dest => dest.ShippingTypeId,opt => opt.MapFrom(src => src.ShippingTypeId))
                      .ForMember(dest => dest.ShippingTypeName,opt => opt.MapFrom(src => src.ShippingType != null ? src.ShippingType.Name : string.Empty))
                      .ForMember(dest => dest.ShippingTypeBaseCost,opt => opt.MapFrom(src => src.ShippingType != null ? src.ShippingType.BaseCost : 0))
                      .ForMember(dest => dest.ShippingTypeDuration,opt => opt.MapFrom(src => src.ShippingType != null ? src.ShippingType.Duration : 0))

                      // Payment Type
                      .ForMember(dest => dest.PaymentType,opt => opt.MapFrom(src => src.PaymentType))
                      .ForMember(dest => dest.PaymentTypeName,opt => opt.MapFrom(src => src.PaymentType != null ? src.PaymentType.ToString() : string.Empty))

                      // Merchant Info
                      .ForMember(dest => dest.MerchantId,opt => opt.MapFrom(src => src.MerchantId))
                      .ForMember(dest => dest.MerchantName,opt => opt.Ignore())

                      // Customer Info
                      .ForMember(dest => dest.CustomerName,opt => opt.MapFrom(src => src.CustomerName))
                      .ForMember(dest => dest.CustomerPhone1,opt => opt.MapFrom(src => src.CustomerPhone1))
                      .ForMember(dest => dest.CustomerAddress,opt => opt.MapFrom(src => src.CustomerAddress))
                      .ForMember(dest => dest.CustomerEmail,opt => opt.MapFrom(src => src.CustomerEmail))

                      // Products Mapping
                      .ForMember(dest => dest.Products,opt => opt.MapFrom(src => src.Products != null ?
                          src.Products.Select(p => new ProductDTO
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Weight = p.Weight,
                              Quantity = p.Quantity,
                              CreatedAt = p.CreatedAt
                          }).ToList() : new List<ProductDTO>()))

                      // Other Properties
                      .ForMember(dest => dest.IsDeleted,opt => opt.Ignore()).ReverseMap(); 
            #endregion
             
        }
    }
}
