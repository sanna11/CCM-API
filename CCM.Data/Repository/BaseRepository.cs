using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> 
        where T : class, IBaseEntity
    {
        protected DbSet<T> CCBDBContext { get; set; }

        public BaseRepository(DbSet<T> ccbDBContext)
        {
            this.CCBDBContext = ccbDBContext;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.Run(() => CCBDBContext.Where(x => !x.IsDeleted)).ConfigureAwait(true);
        }

        public async Task<IQueryable<T>> GetAllAsyncUsingRawQuery(String sqlQuery)
        {
            return await Task.Run(() => CCBDBContext.FromSqlRaw(sqlQuery)).ConfigureAwait(true);
        }

        public async Task<T> FindAsync(object keys)
        {
            var entity = await CCBDBContext.FindAsync(keys);
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            var entity = await CCBDBContext.Where(x => x.InternalId.Equals(id)).FirstOrDefaultAsync().ConfigureAwait(true);
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await (await GetAllAsync().ConfigureAwait(true)).Where(expression).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        public async Task<IQueryable<T>> ListByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return (await GetAllAsync().ConfigureAwait(true)).Where(expression);
        }

        public async Task<T> CreateAsync(T entity)
        {
            return (await this.CCBDBContext.AddAsync(entity)).Entity;
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await CCBDBContext.AddRangeAsync(entities).ConfigureAwait(true);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return (await Task.Run(() => CCBDBContext.Update(entity)).ConfigureAwait(true)).Entity;
        }

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => CCBDBContext.UpdateRange(entities)).ConfigureAwait(true);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity).ConfigureAwait(true);
            }
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                foreach (T entity in entities)
                {
                    entity.IsDeleted = true;
                }
                await UpdateListAsync(entities).ConfigureAwait(true);
            }
        }
    }
}
