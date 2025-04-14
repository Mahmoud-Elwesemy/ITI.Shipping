using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Order;
using ITI.Shipping.Core.Application.Abstraction.OrderReport;
using ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.OrderReportServices
{
    internal class OrderReportService:IOrderReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderReportService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderReportDTO>> GetAllOrderReportAsync(Pramter pramter)
        {
            return _mapper.Map<IEnumerable<OrderReportDTO>>(await _unitOfWork.GetRepository<OrderReport,int>().GetAllAsync(pramter));
        }

        public async Task<OrderReportDTO> GetOrderReportAsync(int id)
        {
            return _mapper.Map<OrderReportDTO>(await _unitOfWork.GetRepository<OrderReport,int>().GetByIdAsync(id));
        }

        public async Task AddAsync(OrderReportDTO DTO)
        {
            await _unitOfWork.GetRepository<OrderReport,int>().AddAsync(_mapper.Map<OrderReport>(DTO));
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(OrderReportDTO DTO)
        {
            var OrderReportRepo = _unitOfWork.GetRepository<OrderReport,int>();

            var existingOrderReport = await OrderReportRepo.GetByIdAsync(DTO.Id);
            if(existingOrderReport == null)
                throw new KeyNotFoundException($"OrderReport with ID {DTO.Id} not found.");

            _mapper.Map(DTO,existingOrderReport); // Update existing entity with DTO data

            OrderReportRepo.UpdateAsync(existingOrderReport);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var OrderReportRepo = _unitOfWork.GetRepository<OrderReport,int>();

            var existingOrderReport = await OrderReportRepo.GetByIdAsync(id);
            if(existingOrderReport == null)
                throw new KeyNotFoundException($"OrderReport with ID {id} not found.");
            await OrderReportRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }


    }
}
