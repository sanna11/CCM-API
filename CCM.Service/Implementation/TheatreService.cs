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

namespace CCM.Service.Implementation
{
    public class TheatreService : BaseService<TheatreViewModel, Theatre>, ITheatreService
    {
        private IBaseRepository<Theatre> TheatreRepo { get; set; }
        private IBaseRepository<TheatreHall> TheatreHallRepo { get; set; }

        public TheatreService(IBaseRepository<Theatre> repo, IBaseRepository<TheatreHall> theatreHallRepo,
            IMapper mapper, IUnitOfWork uow)
            :base(mapper, uow, repo)
        {
            this.TheatreRepo = repo;
            this.TheatreHallRepo = theatreHallRepo;
        }

        public async Task<bool> SeedDataAsync()
        {
            List<Theatre> theatres = new List<Theatre>();

            theatres.Add(new Theatre()
            {
                Name = "Concord - Dehiwala",
                Address = "No 139, Galle Road, Dehiwela",
                EmailAddress = "concord@eapmovies.com",
                Telephone = "+94117549630",
                HasParking = true
            });

            theatres.Add(new Theatre()
            {
                Name = "Savoy - Wellawatte",
                Address = "No 12, Savoy Building, Wellwatte, Colombo 6",
                EmailAddress = "savoymanager@eapmovies.com",
                Telephone = "+94117444466",
                HasParking = true
            });

            theatres.Add(new Theatre()
            {
                Name = "Savoy Premiere (Roxy Cinema) - Wellawatte",
                Address = "	Ramakrishna Road",
                EmailAddress = "savoypremiere@eapmovies.com",
                Telephone = "+94117446223",
                HasParking = true
            });

            theatres.Add(new Theatre()
            {
                Name = "Liberty By Scope Cinemas",
                Address = "No. 35 , Srimath Anagarika Dharmapala Mawatha, Colombo 3",
                EmailAddress = "managerlcc@scopecinemas.com",
                Telephone = "+94112325266",
                HasParking = true
            });

            theatres.Add(new Theatre()
            {
                Name = "Majestic Cineplex",
                Address = "10, Station Road, Colombo 04",
                EmailAddress = "",
                Telephone = "+94115999999",
                HasParking = true
            });

            await TheatreRepo.CreateRangeAsync(theatres).ConfigureAwait(true);

            await UOW.Commit().ConfigureAwait(true);

            return true;
        }
    }
}
