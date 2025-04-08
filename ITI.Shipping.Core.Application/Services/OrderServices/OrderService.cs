using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.OrderServices
{
    internal class OrderService:IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(IUnitOfWork unitOfWork , IMapper mapper ,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrderWithProductsDto>> GetOrdersAsync(Pramter pramter)
        {
            var orders = await _unitOfWork.GetOrderRepository().GetAllAsync(pramter);
            var ordersDto = _mapper.Map<IEnumerable<OrderWithProductsDto>>(orders);
            foreach(var order in ordersDto)
            {
                var MerchantName = await _userManager.FindByIdAsync(order.MerchantName);
                order.MerchantName = MerchantName!.FullName;
            }
            return ordersDto;
        }
        public async Task<OrderWithProductsDto> GetOrderAsync(int id)
        {
            var findOrder = await _unitOfWork.GetOrderRepository().GetByIdAsync(id);

            if(findOrder == null || findOrder.IsDeleted)
            {
                return null;
            }

            var orderDto = _mapper.Map<OrderWithProductsDto>(findOrder);
            var MerchantName = await _userManager.FindByIdAsync(orderDto.MerchantName);
            orderDto.MerchantName = MerchantName!.FullName;
            return orderDto;
        }

        public async Task AddAsync(addOrderDto DTO ,Pramter? pramter)
        {
            decimal ShippingCost = 0;
            decimal Ordercost = DTO.OrderCost;
            decimal Totalweight = DTO.TotalWeight;
            var IsOutOfCityShipping = DTO.IsOutOfCityShipping;

            var city = await _unitOfWork.GetRepository<CitySetting,int>().GetByIdAsync(DTO.City);
            var pickupShippingCost = city?.pickupShippingCost;
            decimal standardShippingCost = city!.StandardShippingCost;

            var IEnumerableweightsetting = await _unitOfWork.GetRepository<WeightSetting,int>().GetAllAsync(pramter);
            var weightsetting = IEnumerableweightsetting.FirstOrDefault();
            //var MinWeight = weightsetting.MinWeight;
            decimal MaxWeight = weightsetting!.MaxWeight;
            decimal CostPerKG = weightsetting!.CostPerKg;

            var SpecialCityCost = await _unitOfWork.GetSpecialCityCostRepository()
                .GetCityCostByMarchantId(DTO.MerchantName , DTO.City);
          
            if(SpecialCityCost != null )
            {
                if(Totalweight > 0 && Totalweight <= MaxWeight)
                {
                    ShippingCost += SpecialCityCost.Price;
                }
                else if(Totalweight > MaxWeight)
                {
                    decimal ExcessWeight = Totalweight - MaxWeight;
                    ShippingCost += SpecialCityCost.Price + (ExcessWeight * CostPerKG);
                }

               if(IsOutOfCityShipping == true)
                {
                    ShippingCost = ShippingCost*1.1m;
                }
            }
            else
            {
                if (Totalweight > 0 && Totalweight <= MaxWeight)
                {
                    ShippingCost += standardShippingCost;
                }
                else if (Totalweight > MaxWeight)
                {
                    decimal ExcessWeight = Totalweight - MaxWeight;
                    ShippingCost += standardShippingCost + (ExcessWeight * CostPerKG);
                }
               
                if(IsOutOfCityShipping == true)
                {
                    ShippingCost = ShippingCost * 1.1m;
                }
            }

            var ShippingType = await _unitOfWork.GetRepository<ShippingType,int>().GetByIdAsync(DTO.ShippingId);
            if(ShippingType != null)
            {
                ShippingCost += ShippingType.BaseCost;
            }
            DTO.ShippingCost = ShippingCost;

            await _unitOfWork.GetOrderRepository().AddAsync(_mapper.Map<Order>(DTO));
            await _unitOfWork.CompleteAsync();
        }
        public async Task UpdateAsync(updateOrderDto DTO)
        {
            var OrderRepo = _unitOfWork.GetOrderRepository();
            var existingOrder = await OrderRepo.GetByIdAsync(DTO.Id);
            if(existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {DTO.Id} not found.");
            _mapper.Map(DTO,existingOrder);
            OrderRepo.UpdateAsync(existingOrder);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var OrderRepo = _unitOfWork.GetOrderRepository();
            var existingOrder = await OrderRepo.GetByIdAsync(id);
            if(existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            await OrderRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
           
        }

        public async Task<IEnumerable<OrderWithProductsDto>> GetOrdersByStatus(OrderStatus status)
        {
            var orders = await _unitOfWork.GetOrderRepository().GetOrdersByStatus(status);

            var OrdersDto = _mapper.Map<IEnumerable<OrderWithProductsDto>>(orders);
            foreach(var order in OrdersDto)
            {
                var MerchantName = await _userManager.FindByIdAsync(order.MerchantName);
                order.MerchantName = MerchantName!.FullName;
            }
            return OrdersDto;
        }
    }
}
