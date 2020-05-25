using Microsoft.EntityFrameworkCore;
using SampleWithDotNetCoreAndAngular.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        CoreLearningContext _dbContext;
        public GenericRepository(CoreLearningContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = _dbContext.Set<T>();
            return await result.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);
            return item;
        }

        public async Task InsertAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T t)
        {
            _dbContext.Set<T>().Attach(t);
            _dbContext.Entry(t).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
