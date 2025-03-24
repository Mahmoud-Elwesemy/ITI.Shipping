using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.Order.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
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

        public OrderService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            return _mapper.Map<IEnumerable<OrderDTO>>(await _unitOfWork.GetRepository<Order,int>().GetAllAsync());
        }
        public async Task<OrderDTO> GetOrderAsync(int id)
        {
            return _mapper.Map<OrderDTO>(await _unitOfWork.GetRepository<Order,int>().GetByIdAsync(id));
        }

        public async Task AddAsync(OrderDTO DTO)
        {
            await _unitOfWork.GetRepository<Order,int>().AddAsync(_mapper.Map<Order>(DTO));
            await _unitOfWork.CompleteAsync();
        }
        public async Task UpdateAsync(OrderDTO DTO)
        {
            var OrderRepo = _unitOfWork.GetRepository<Order,int>();
            var existingOrder = await OrderRepo.GetByIdAsync(DTO.Id);
            if(existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {DTO.Id} not found.");
            _mapper.Map(DTO,existingOrder);
            OrderRepo.UpdateAsync(existingOrder);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var OrderRepo = _unitOfWork.GetRepository<Order,int>();
            var existingOrder = await OrderRepo.GetByIdAsync(id);
            if(existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            await OrderRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
           
        }


    }
}
