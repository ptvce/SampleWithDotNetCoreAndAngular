using SampleWithDotNetCoreAndAngular.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        Task InsertAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<PaginationResult<T>> GetAsQueryableAsync(Expression<Func<T,bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties="", int? page=null, int? limit = null);
        Task CommitAsync();
    }
}
