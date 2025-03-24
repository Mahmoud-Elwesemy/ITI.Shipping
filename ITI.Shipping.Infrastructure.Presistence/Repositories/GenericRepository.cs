using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Infrastructure.Presistence.Repositories
{
    public class GenericRepository<T , Tkey>:IGenericRepository<T , Tkey> where T : class where Tkey : IEquatable<Tkey>
    {
        
        
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Tkey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task DeleteAsync(Tkey id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        // This Is A Generic Repository  That Contains The Basic CRUD Operations 
        // That Can Be Performed On Any Entity Without ---- User Entity ----  


        //public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        //{
        //    return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.Id == id);
        //}
    }
}
