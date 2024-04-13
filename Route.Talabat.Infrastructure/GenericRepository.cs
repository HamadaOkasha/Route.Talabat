using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
             if (typeof(T) == typeof(Product))
                 return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p=>p.Brand).Include(p => p.Category).ToListAsync();

            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
