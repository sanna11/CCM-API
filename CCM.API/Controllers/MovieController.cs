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
    public class MovieController : BaseApiCRUDController<MovieViewModel>
    {
        public MovieController(IMovieService service)
           : base(service)
        {

        }
    }
}