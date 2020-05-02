using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CCM.Service.ViewModels;
using CCM.Service.Interface;

namespace CCM.API.Controllers
{
    public class TheatreController : BaseApiCRUDController<TheatreViewModel>
    {
        public TheatreController(ITheatreService service)
            :base(service)
        {

        }
    }
}