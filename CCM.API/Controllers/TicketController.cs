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
    public class TicketController : BaseAPIController
    {
        protected ITicketService Service { get; set; }

        public TicketController(ITicketService service)
        {
            this.Service = service;
        }

        [HttpGet("validate")]
        public async Task<ActionResult<BaseResultViewModel>> ValidateTicket([FromQuery] TicketValidateModel model)
        {
            return Ok(await Service.ValidateTicket(model).ConfigureAwait(true));
        }
    }
}