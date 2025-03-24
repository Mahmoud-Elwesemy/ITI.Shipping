using ITI.Shipping.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Domin.Repositories.contract
{
    public interface IGenericRepository<T,Tkey> where T : class where Tkey : IEquatable<Tkey>
    {
        // This Is A Generic Repository Interface That Contains The Basic CRUD Operations 
        // That Can Be Performed On Any Entity Without ---- User Entity ----     
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Tkey id);

        //Task <ApplicationUser?> GetUserByIdAsync(string id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task DeleteAsync(Tkey id);
    }
}
