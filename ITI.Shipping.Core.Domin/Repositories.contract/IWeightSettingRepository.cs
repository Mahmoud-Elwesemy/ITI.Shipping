using ITI.Shipping.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Domin.Repositories.contract;
public interface IWeightSettingRepository:IGenericRepository<WeightSetting,int>
{
    Task<IEnumerable<WeightSetting>> GetAllWeightSetting();
}
