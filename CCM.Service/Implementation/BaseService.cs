using AutoMapper;
using CCM.Data.Models;
using CCM.Data.Repository;
using CCM.Data.UOW;
using CCM.Service.Interface;
using CCM.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Service.Implementation
{
    public abstract class BaseService<TObject, TEntity> : IBaseService<TObject>
        where TObject : class, IBaseViewModel
        where TEntity : class, IBaseEntity
    {
        protected IMapper Mapper { get; set; }
        protected IUnitOfWork UOW { get; set; }
        protected IBaseRepository<TEntity> Repo { get; set; }

        public BaseService(IMapper mapper, IUnitOfWork uow, IBaseRepository<TEntity> repo)
        {
            this.Mapper = mapper;
            this.Repo = repo;
            this.UOW = uow;
        }

        public virtual async Task<TObject> AddNewEntityAsync(TObject tobject)
        {
            var entity = Mapper.Map<TEntity>(tobject);
            var result = await Repo.CreateAsync(entity).ConfigureAwait(true);
            await UOW.Commit().ConfigureAwait(true);
            return Mapper.Map<TObject>(result);
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            var curObj = await Repo.FindByIdAsync(id).ConfigureAwait(true);
            if (curObj != null)
            {
                await Repo.DeleteAsync(curObj).ConfigureAwait(true);
                await UOW.Commit().ConfigureAwait(true);
            }
        }

        public virtual async Task<IEnumerable<TObject>> GetAllEntitiesAsync()
        {
            return Mapper.Map<IEnumerable<TObject>>(await Repo.GetAllAsync().ConfigureAwait(true));
        }

        public async Task<TObject> GetEntityById(Guid id)
        {
            return Mapper.Map<TObject>(await Repo.FindByIdAsync(id).ConfigureAwait(true));
        }

        public async Task<TObject> UpdateEntityAsync(Guid id, TObject tobject)
        {
            var curObj = await Repo.FindByIdAsync(id).ConfigureAwait(true);
            if (curObj == null)
            {
                return null;
            }
            var result = await Repo.UpdateAsync(Mapper.Map(tobject, curObj)).ConfigureAwait(true);
            await UOW.Commit().ConfigureAwait(true);
            return Mapper.Map<TObject>(result);
        }
    }
}
