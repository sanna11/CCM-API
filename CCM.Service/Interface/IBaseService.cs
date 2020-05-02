using CCM.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Service.Interface
{
    public interface IBaseService<TObject>
        where TObject : class, IBaseViewModel
    {
        Task<IEnumerable<TObject>> GetAllEntitiesAsync();

        Task<TObject> GetEntityById(Guid id);

        Task<TObject> AddNewEntityAsync(TObject category);

        Task<TObject> UpdateEntityAsync(Guid id, TObject category);

        Task DeleteEntityAsync(Guid id);
    }
}
