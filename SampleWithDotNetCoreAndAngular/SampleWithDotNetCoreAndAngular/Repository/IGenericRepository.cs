using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        Task InsertAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
