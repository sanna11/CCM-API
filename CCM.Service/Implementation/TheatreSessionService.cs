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
    public class TheatreSessionService : BaseService<TheatreSessionViewModel, TheatreSession>, ITheatreSessionService
    {
        private IBaseRepository<TheatreSession> TheatreSessionRepo { get; set; }

        public TheatreSessionService(IBaseRepository<TheatreSession> repo, 
            IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow, repo)
        {
            this.TheatreSessionRepo = repo;
        }

        public async Task<bool> SeedDataAsync()
        {
            List<TheatreSession> sessions = new List<TheatreSession>();

            sessions.Add(new TheatreSession()
            {
                ComplimentTickets = 0,
                Date = new DateTime(2020,02,01),
                Income = 85900,
                StartTime = "16:00",
                EndTime = "19:20",
                MovieId = 1,
                TheatreId = 1,
                TotalBookedTicket = 104,
                UsedTicket = 92,
                TotalTickets = 104
            });

            sessions.Add(new TheatreSession()
            {
                ComplimentTickets = 0,
                Date = new DateTime(2020, 02, 01),
                Income = 101235,
                StartTime = "19:35",
                EndTime = "22:15",
                MovieId = 1,
                TheatreId = 1,
                TotalBookedTicket = 132,
                UsedTicket = 122,
                TotalTickets = 132
            });

            sessions.Add(new TheatreSession()
            {
                ComplimentTickets = 0,
                Date = new DateTime(2020, 02, 01),
                Income = 85425,
                StartTime = "19:15",
                EndTime = "21:50",
                MovieId = 1,
                TheatreId = 3,
                TotalBookedTicket = 88,
                UsedTicket = 80,
                TotalTickets = 88
            });


            sessions.Add(new TheatreSession()
            {
                ComplimentTickets = 5,
                Date = new DateTime(2020, 02, 02),
                Income = 55000,
                StartTime = "16:30",
                EndTime = "18:50",
                MovieId = 3,
                TheatreId = 2,
                TotalBookedTicket = 45,
                UsedTicket = 40,
                TotalTickets = 50
            });

            sessions.Add(new TheatreSession()
            {
                ComplimentTickets = 0,
                Date = new DateTime(2020, 02, 01),
                Income = 102654,
                StartTime = "19:35",
                EndTime = "22:15",
                MovieId = 3,
                TheatreId = 3,
                TotalBookedTicket = 95,
                UsedTicket = 92,
                TotalTickets = 95
            });

            await TheatreSessionRepo.CreateRangeAsync(sessions).ConfigureAwait(true);

            await UOW.Commit().ConfigureAwait(true);

            return true;
        }
    }
}
