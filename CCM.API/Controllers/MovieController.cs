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

        [HttpGet("by-theatre")]
        public async Task<ActionResult<ConcessionSalesViewModel>> OccupancyBySeason([FromQuery] MovieRequestViewModel model)
        {
            return Ok(await ((IMovieService) Service).GetMoviesByTheatre(model).ConfigureAwait(true));
        }
    }
}