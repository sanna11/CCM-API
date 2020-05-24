using System;
using System.Collections.Generic;
using System.Text;
using CCM.Data.Models;
using CCM.Data.Repository;
using CCM.Data.UOW;
using CCM.Service.ViewModels;
using CCM.Service.Interface;
using AutoMapper;
using System.Threading.Tasks;
using CCM.Core.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCM.Service.Implementation
{
    public class MovieService : BaseService<MovieViewModel, Movie>, IMovieService
    {
        private IBaseRepository<Movie> MovieRepo { get; set; }
        private IBaseRepository<TheatreSession> TheatreSessionRepo { get; set; }

        public MovieService(IBaseRepository<Movie> repo, IBaseRepository<TheatreSession> theatreSessionRepo,
            IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow, repo)
        {
            this.MovieRepo = repo;
            this.TheatreSessionRepo = theatreSessionRepo;
        }

        public async Task<IEnumerable<MovieViewModel>> GetMoviesByTheatre(MovieRequestViewModel requestModel)
        {
            return Mapper.Map<IEnumerable<MovieViewModel>>(await (await TheatreSessionRepo.ListByConditionAsync(x => (requestModel.TheatreId == 0 ||
            x.TheatreId == requestModel.TheatreId) && (requestModel.Date == null  || x.Date == requestModel.Date)))
                .Include(x => x.MovieInfo)
                .Select(x => x.MovieInfo).ToListAsync());
        }


        public async Task<bool> SeedDataAsync()
        {
            List<Movie> movies = new List<Movie>();

            movies.Add(new Movie()
            {
                Name = "Darbar",
                HasSubtitles = true,
                IsThreeD = false,
                Language = LanguageEnum.Tamil,
                ReleaseDate = new DateTime(2020, 1, 20),
                Ratings = 6.4,
                ScreeningDuration = 159
            });

            movies.Add(new Movie()
            {
                Name = "1917",
                HasSubtitles = true,
                IsThreeD = false,
                Language = LanguageEnum.English,
                ReleaseDate = new DateTime(2019, 12, 4),
                Ratings = 8.4,
                ScreeningDuration = 119
            });

            movies.Add(new Movie()
            {
                Name = "Bad Boys for Life",
                HasSubtitles = true,
                IsThreeD = true,
                Language = LanguageEnum.English,
                ReleaseDate = new DateTime(2020, 1, 20),
                Ratings = 7.2,
                ScreeningDuration = 124
            });

            movies.Add(new Movie()
            {
                Name = "Birds of Prey",
                HasSubtitles = true,
                IsThreeD = true,
                Language = LanguageEnum.English,
                ReleaseDate = new DateTime(2020, 01, 29),
                Ratings = 6.5,
                ScreeningDuration = 109
            });

            movies.Add(new Movie()
            {
                Name = "Love Aaj Kal",
                HasSubtitles = true,
                IsThreeD = true,
                Language = LanguageEnum.Hindi,
                ReleaseDate = new DateTime(2020, 02, 14),
                Ratings = 5.7,
                ScreeningDuration = 142
            });

            movies.Add(new Movie()
            {
                Name = "Tsunami",
                HasSubtitles = true,
                IsThreeD = false,
                Language = LanguageEnum.Sinhala,
                ReleaseDate = new DateTime(2020, 01, 16),
                Ratings = 5.7
            });

            await MovieRepo.CreateRangeAsync(movies).ConfigureAwait(true);

            await UOW.Commit().ConfigureAwait(true);

            return true;
        }

    }
}
