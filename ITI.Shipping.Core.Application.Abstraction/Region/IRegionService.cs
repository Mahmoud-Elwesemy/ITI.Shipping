using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Region
{
    public interface IRegionService
    {
        Task<IEnumerable<RegionDto>> GetRegionsAsync();
        Task<RegionDto> GetRegionAsync(int id);
        Task AddAsync(RegionDto DTO);
        Task UpdateAsync(RegionDto DTO);
        Task DeleteAsync(int id);
    }
}
