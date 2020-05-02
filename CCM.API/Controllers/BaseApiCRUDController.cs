using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCM.Service.Interface;
using CCM.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCM.API.Controllers
{
    public abstract class BaseApiCRUDController<TObject> : BaseAPIController
        where TObject : class, IBaseViewModel
    {
        protected IBaseService<TObject> Service { get; set; }
        public BaseApiCRUDController(IBaseService<TObject> service)
        {
            this.Service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TObject>>> GetAll() =>
            Ok(await Service.GetAllEntitiesAsync().ConfigureAwait(true));


        [HttpPost]
        public virtual async Task<ActionResult<TObject>> CreateNew(TObject tobject) =>
            Ok(await Service.AddNewEntityAsync(tobject).ConfigureAwait(true));

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<IEnumerable<TObject>>> GetById(Guid id)
        {
            TObject result = await Service.GetEntityById(id).ConfigureAwait(true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TObject>> Update(Guid id, TObject tobject)
        {
            if (tobject == null || !tobject.InternalId.Equals(id))
            {
                return NotFound();
            }

            TObject result = await Service.UpdateEntityAsync(id, tobject).ConfigureAwait(true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TObject>> Delete(Guid id)
        {
            await Service.DeleteEntityAsync(id).ConfigureAwait(true);
            return Ok();
        }
    }
}