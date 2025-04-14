using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.BranchServices
{
    internal class BranchService:IBranchService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public BranchService(IUnitOfWork UnitOfWork , IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _Mapper = mapper;
        }
        public async Task<IEnumerable<BranchDTO>> GetBranchesAsync(Pramter pramter)
        {
            return _Mapper.Map<IEnumerable<BranchDTO>>(await _UnitOfWork.GetRepository<Branch,int>().GetAllAsync(pramter));
        }
        public async Task<BranchDTO> GetBranchAsync(int id)
        {
            return _Mapper.Map<BranchDTO>(await _UnitOfWork.GetRepository<Branch,int>().GetByIdAsync(id));
        }

        public async Task AddAsync(BranchToAddDTO DTO)
        {
            await _UnitOfWork.GetRepository<Branch,int>().AddAsync(_Mapper.Map<Branch>(DTO));
            await _UnitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(BranchToUpdateDTO DTO)
        {
            var branchRepo = _UnitOfWork.GetRepository<Branch,int>();

            var existingBranch = await branchRepo.GetByIdAsync(DTO.Id);
            if(existingBranch == null)
                throw new KeyNotFoundException($"Branch with ID {DTO.Id} not found.");

            _Mapper.Map(DTO,existingBranch); // Update existing entity with DTO data

            branchRepo.UpdateAsync(existingBranch);
            await _UnitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id) // Accepts only ID
        {
            var branchRepo = _UnitOfWork.GetRepository<Branch,int>();

            var existingBranch = await branchRepo.GetByIdAsync(id);
            if(existingBranch == null)
                throw new KeyNotFoundException($"Branch with ID {id} not found.");

            await branchRepo.DeleteAsync(id); // Call the repository's delete method
            await _UnitOfWork.CompleteAsync();
        }
    }
}
