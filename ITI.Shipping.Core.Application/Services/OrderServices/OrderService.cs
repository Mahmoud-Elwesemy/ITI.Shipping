using AutoMapper;
using Castle.Core.Logging;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IUnitOfWork unitOfWork , IMapper mapper ,UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
           _httpContextAccessor = httpContextAccessor;
        }

       //---------------------------------------------------------------------------
        private async Task<IEnumerable<OrderWithProductsDto>> GetMerchantName(IEnumerable<Order> orders)
        {
            var ordersDto = _mapper.Map<IEnumerable<OrderWithProductsDto>>(orders);
            foreach(var order in ordersDto)
            {
                var MerchantName = await _userManager.FindByIdAsync(order.MerchantName);
                order.MerchantName = MerchantName!.FullName;
            }
            return ordersDto;
        }
       //--------------------------------------------------------------------------


        public async Task<IEnumerable<OrderWithProductsDto>> GetOrdersAsync(Pramter pramter)
        {
            var orders = await _unitOfWork.GetOrderRepository().GetAllAsync(pramter);
            var ordersDto = await GetMerchantName(orders);
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

        public async Task AddAsync(addOrderDto DTO)
        {
            decimal ShippingCost = 0;
            decimal Ordercost = DTO.OrderCost;
            decimal Totalweight = DTO.TotalWeight;
            var IsOutOfCityShipping = DTO.IsOutOfCityShipping;

            var city = await _unitOfWork.GetRepository<CitySetting,int>().GetByIdAsync(DTO.City);
            var pickupShippingCost = city!.pickupShippingCost;
            decimal standardShippingCost = city!.StandardShippingCost;

            var Allweightsetting = await _unitOfWork.GetWeightSettingRepository().GetAllWeightSetting();
            var weightsetting = Allweightsetting.FirstOrDefault();
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
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            if(currentUser != null && await _userManager.IsInRoleAsync(currentUser,DefaultRole.Merchant))
            {
                DTO.status = OrderStatus.WaitingForConfirmation;

            }
            else
            {
                DTO.status = OrderStatus.Pending;
            }
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
        // Get all orders by status
        public async Task<IEnumerable<OrderWithProductsDto>> GetOrdersByStatus(OrderStatus status,Pramter pramter)
        {
            var orders = await _unitOfWork.GetOrderRepository().GetOrdersByStatus(status,pramter);

            var ordersDto = await GetMerchantName(orders);
            return ordersDto;
        }
        //  Get all waiting orders
        public async Task<IEnumerable<OrderWithProductsDto>> GetAllWatingOrder(Pramter pramter)
        {
          var WatingOrder =  await _unitOfWork.GetOrderRepository().GetAllWatingOrder(pramter);
            var WatingordersDto = await GetMerchantName(WatingOrder);
            return WatingordersDto;
        }
        // Change order status to pending
        public async Task ChangeOrderStatusToPending(int id)
        {
            await ChangeOrderStatus(id,OrderStatus.Pending);
        }
        // Change order status to Declined
        public async Task ChangeOrderStatusToDeclined(int id)
        {
            await ChangeOrderStatus(id,OrderStatus.Declined);
        }

        //--------------------------------------------------------------------
        private async Task ChangeOrderStatus(int id , OrderStatus orderStatus)
        {
                var Order = await _unitOfWork.GetOrderRepository().GetByIdAsync(id);
                Order!.Status = orderStatus;
                _unitOfWork.GetOrderRepository().UpdateAsync(Order);
                await _unitOfWork.CompleteAsync();
        }
        // Assign order to courier
        public async Task AssignOrderToCourier(int OrderId,string courierId)
        {
            var Order = await _unitOfWork.GetOrderRepository().GetByIdAsync(OrderId);
            Order!.CourierId = courierId;
            _unitOfWork.GetOrderRepository().UpdateAsync(Order);
            await _unitOfWork.CompleteAsync();
        }
        //-------------------------------------------------------------------
     
    }
}
