using Microsoft.EntityFrameworkCore;
using SampleWithDotNetCoreAndAngular.Common;
using SampleWithDotNetCoreAndAngular.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginationResult<T>> GetAsQueryableAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null, int? limit = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                var array = includeProperties.Split(',');
                foreach (var item in array)
                {
                    query.Include(item);
                }

            }

            if (limit != null && page == null)
                query = query.Take(limit.Value);

            if (limit != null && page != null)
            {
                int skip = (page.Value - 1) * limit.Value;
                query = query.Skip(skip).Take(limit.Value);

            }

            var result = new PaginationResult<T>();
            result.Data = query;
            result.Page = page;
            result.Limit = limit;
            result.Total = filter != null ? await _dbContext.Set<T>().Where(filter).CountAsync() : await _dbContext.Set<T>().CountAsync() ;
            result.Pages = limit != null ? Convert.ToInt32(Math.Ceiling((decimal)result.Total / limit.Value)) : (int?)null;

            return result;
        }
    }
}
