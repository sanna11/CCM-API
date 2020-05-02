using CCM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Data.Repository
{
    public interface IBaseRepository<T>
        where T : class, IBaseEntity
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> FindAsync(object keys);
        Task<IQueryable<T>> GetAllAsyncUsingRawQuery(String sqlQuery);
        Task<T> FindByIdAsync(Guid id);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> ListByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task UpdateListAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteListAsync(IEnumerable<T> entities);
    }
}
