using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CCM.Service.ViewModels;

namespace CCM.Service.Interface
{
    public interface IMovieService : IBaseService<MovieViewModel>
    {
        Task<bool> SeedDataAsync();
        Task<IEnumerable<MovieViewModel>> GetMoviesByTheatre(MovieRequestViewModel requestModel);
    }
}
