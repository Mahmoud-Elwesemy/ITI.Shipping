using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.SpecialCityCostServices
{
    internal class SpecialCityCostService:ISpecialCityCostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpecialCityCostService(IUnitOfWork unitOfWork , IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SpecialCityCostDTO>> GetAllSpecialCityCostAsync()
        {
            return _mapper.Map<IEnumerable<SpecialCityCostDTO>>(await _unitOfWork.GetRepository<SpecialCityCost,int>().GetAllAsync());
        }
        public async Task<SpecialCityCostDTO> GetSpecialCityCostAsync(int id)
        {
            return _mapper.Map<SpecialCityCostDTO>(await _unitOfWork.GetRepository<SpecialCityCost,int>().GetByIdAsync(id));
        }
        public async Task AddAsync(SpecialCityCostDTO DTO)
        {
            var citySettingExists = await _unitOfWork.GetRepository<CitySetting,int>()
            .GetByIdAsync(DTO.CitySettingId);

            if(citySettingExists == null)
            {
                throw new KeyNotFoundException($"CitySetting with ID {DTO.CitySettingId} does not exist.");
            }

            await _unitOfWork.GetRepository<SpecialCityCost,int>()
                .AddAsync(_mapper.Map<SpecialCityCost>(DTO));

            await _unitOfWork.CompleteAsync();
        }
        public async Task UpdateAsync(SpecialCityCostDTO DTO)
        {
            var SpecialCityCostRepo =  _unitOfWork.GetRepository<SpecialCityCost,int>();
            var existingSpecialCityCost = await SpecialCityCostRepo.GetByIdAsync(DTO.Id);
            if(existingSpecialCityCost == null)
            {
                throw new KeyNotFoundException($"SpecialCityCost with ID {DTO.Id} not found.");
            }
            _mapper.Map(DTO,existingSpecialCityCost);
            SpecialCityCostRepo.UpdateAsync(existingSpecialCityCost);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var SpecialCityCostRepo =  _unitOfWork.GetRepository<SpecialCityCost,int>();
            var existingSpecialCityCost =await SpecialCityCostRepo.GetByIdAsync(id);
            if (existingSpecialCityCost == null)
            {
                throw new KeyNotFoundException($"SpecialCityCost with ID {id} not found.");
            }
            await SpecialCityCostRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }


    }
}
