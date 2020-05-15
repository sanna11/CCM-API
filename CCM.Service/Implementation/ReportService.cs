using CCM.Service.Interface;
using CCM.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCM.Data.Models;
using CCM.Data.Repository;
using CCM.Data.UOW;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CCM.Core.Constants;
using CCM.Core.Enum;

namespace CCM.Service.Implementation
{
    public class ReportService :IReportService
    {
        protected IMapper Mapper { get; set; }
        protected IUnitOfWork UOW { get; set; }
        protected IBaseRepository<TheatreSession> Repo { get; set; }
        protected IBaseRepository<TicketSale> TicketSaleRepo { get; set; }
        protected IBaseRepository<Ticket> TicketRepo { get; set; }

        public ReportService(IMapper mapper, IUnitOfWork uow, IBaseRepository<TheatreSession> repo,
             IBaseRepository<TicketSale> ticketSaleRepo, IBaseRepository<Ticket> ticketRepo)
        {
            this.Mapper = mapper;
            this.Repo = repo;
            this.UOW = uow;

            this.TicketRepo = ticketRepo;
            this.TicketSaleRepo = ticketSaleRepo;
        }


        public async Task<TheatreSalesViewModel> GetBoxOfficeSummary(BoxOfficeRequestViewModel requestModel)
        {
            if(requestModel == null)
            {
                return new TheatreSalesViewModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }

            List<TheatreSession> sessions =  await (await Repo.ListByConditionAsync(x => x.Date >= requestModel.StartDate.Date && x.Date 
            <= requestModel.EndDate.Date && (requestModel.TheatreId < 1 || x.TheatreId == requestModel.TheatreId)).ConfigureAwait(true))
                .Include(x => x.MovieInfo).ToListAsync().ConfigureAwait(true);

            List<SaleViewModel> sales = sessions
                .GroupBy(x => x.MovieId)
                .Select(x => new SaleViewModel ()
                {
                    MovieName = $@"{x.First().MovieInfo.Name} ({LanguageConstants.GetString(x.First().MovieInfo.Language)}) {((x.First().MovieInfo.IsThreeD) ? "3D" : "2D")}",
                    ComplimentTickets = x.Sum(g=> g.ComplimentTickets.GetValueOrDefault()),
                    BoxOffice = x.Sum(g => g.Income.GetValueOrDefault()),
                    TotalBookedTicket = x.Sum(g => g.TotalBookedTicket.GetValueOrDefault())
                }).ToList();

            return new TheatreSalesViewModel()
            {
                IsSuccess = true,
                TheatreSales = sales,
                BoxOffice = sales.Sum(x => x.BoxOffice),
                ComplimentTickets = sales.Sum(x => x.ComplimentTickets),
                TotalBookedTicket = sales.Sum(x => x.ComplimentTickets),
                TotalTickets = sales.Sum(x => x.ComplimentTickets)
            };
        }

        public async Task<ConcessionSalesViewModel> ConcessionSalesReport(BoxOfficeRequestViewModel requestModel)
        {
            if (requestModel == null)
            {
                return new ConcessionSalesViewModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }

            List<TheatreSession> sessions = await (await Repo.ListByConditionAsync(x => x.Date >= requestModel.StartDate.Date && x.Date
           <= requestModel.EndDate.Date && (requestModel.TheatreId < 1 || x.TheatreId == requestModel.TheatreId)).ConfigureAwait(true))
                .Include(x => x.MovieInfo).ToListAsync().ConfigureAwait(true);

            List<ConcessionSalesItemViewModel> sales = sessions
                .GroupBy(x => x.MovieId)
                .Select(x => new ConcessionSalesItemViewModel()
                {
                    MovieName = $@"{x.First().MovieInfo.Name} ({LanguageConstants.GetString(x.First().MovieInfo.Language)}) {((x.First().MovieInfo.IsThreeD) ? "3D" : "2D")}",
                    ConcessionSales = x.Sum(g => g.ConcessSales),
                    ConcessionCost = x.Sum(g => g.ConcessCost),
                    ConcessionTransactions = x.Sum(g => g.ConcessTrans),
                    BoxOffice = x.Sum(g => g.Income.GetValueOrDefault()),
                    Admits = x.Sum(g => g.UsedTicket.GetValueOrDefault()) + x.Sum(g => g.ComplimentTickets.GetValueOrDefault())
                }).ToList();

            return new ConcessionSalesViewModel()
            {
                IsSuccess = true,
                ConcessionSales = sales
            };
        }

        public async Task<OccupancyViewModel> OccupancyPercentage(BoxOfficeRequestViewModel requestModel)
        {
            if (requestModel == null)
            {
                return new OccupancyViewModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }

            List<TheatreSession> sessions = await (await Repo.ListByConditionAsync(x => x.Date >= requestModel.StartDate.Date && x.Date
           <= requestModel.EndDate.Date).ConfigureAwait(true))
                .Include(x => x.MovieInfo).Include(x=> x.TheatreInfo).Include(x=> x.TheatreHallInfo).ToListAsync().ConfigureAwait(true);

            List<OccupancyPerMovie> occupancy = sessions
                .GroupBy(x => x.MovieId)
                .Select(x => new OccupancyPerMovie()
                {
                    MovieName = $@"{x.First().MovieInfo.Name} ({LanguageConstants.GetString(x.First().MovieInfo.Language)}) {((x.First().MovieInfo.IsThreeD) ? "3D" : "2D")}",
                    OccupancyPercs = x.Select(g=> new OccupancyPercViewModel()
                    {
                        Date = g.Date,
                        StartTime = g.StartTime,
                        TheartreHalls = $"{g.TheatreInfo?.Name} {g.TheatreHallInfo?.Name}",
                        TotalTickets = g.TotalTickets.GetValueOrDefault(),
                        UsedTickets = g.UsedTicket.GetValueOrDefault() + g.ComplimentTickets.GetValueOrDefault()
                    })
                }).ToList();

            return new OccupancyViewModel()
            {
                IsSuccess = true,
                Occupancy = occupancy
            };
        }


        public async Task<DailyCollectionReportModel> DailyCollectionReport(DailyCollectionRequestViewModel requestModel)
        {
            if (requestModel == null)
            {
                return new DailyCollectionReportModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }
            //List<TheatreSession> sessions = await (await Repo.ListByConditionAsync(x => x.Date >= requestModel.Date &&
            //       x.MovieId == requestModel.MovieId && x.TheatreId == requestModel.TheatreId).ConfigureAwait(true))
            //    .Include(x => x.MovieInfo).Include(x => x.TheatreInfo).Include(x => x.TheatreHallInfo).ToListAsync().ConfigureAwait(true);

            //foreach(var session in sessions)
            //{
            //    (await TicketRepo.GetAllAsync()
            //}

            List<Ticket> results = await (await TicketRepo.GetAllAsync().ConfigureAwait(true))
               .Include(x=> x.TicketSaleInfo).ThenInclude(x=> x.TheatreSessionInfo)
              .ThenInclude(x => x.MovieInfo).Include(x => x.TicketSaleInfo.TheatreSessionInfo.TheatreInfo)
              .Include(x => x.TicketSaleInfo.TheatreSessionInfo.TheatreHallInfo)
              .Where(x=> x.TicketSaleInfo.TheatreSessionInfo.Date >= requestModel.Date &&
                   x.TicketSaleInfo.TheatreSessionInfo.MovieId == requestModel.MovieId && 
                   x.TicketSaleInfo.TheatreSessionInfo.TheatreId == requestModel.TheatreId).ToListAsync().ConfigureAwait(true);

            List<SessionSalesViewModel> sessionSales = results
                .GroupBy(x => new { x.TicketSaleInfo.TheatreSessionId, x.TicketType })
                .Select(x => new SessionSalesViewModel()
                {
                    SessionStart = x.First().TicketSaleInfo.TheatreSessionInfo.StartTime,
                    Amount = x.Sum(g=> g.TicketPrice),
                    ClassName = ((TicketTypeEnum) x.Key.TicketType).ToString(),
                    Sold = x.Count(),
                }).ToList();

            return new DailyCollectionReportModel()
            {
                IsSuccess = true,
                Result = results.Count == 0 ? new DailyCollectionResult() :  new DailyCollectionResult()
                {
                    MovieName = $@"{results.First().TicketSaleInfo.TheatreSessionInfo.MovieInfo.Name} ({LanguageConstants.GetString(results.First().TicketSaleInfo.TheatreSessionInfo.MovieInfo.Language)}) {((results.First().TicketSaleInfo.TheatreSessionInfo.MovieInfo.IsThreeD) ? "3D" : "2D")}",
                    Date = requestModel.Date,
                    Theatre = $"{results.First().TicketSaleInfo.TheatreSessionInfo.TheatreInfo?.Name} " +
                    $"{results.First().TicketSaleInfo.TheatreSessionInfo.TheatreHallInfo?.Name}",
                    Sales = sessionSales,
                    TotalSales = sessionSales.Sum(x=> x.Amount),

                }
            };
        }

        public async Task<TicketSalesResponseModel> TicketSales(TicketSalesTodayRequestModel requestModel)
        {
            if (requestModel == null)
            {
                return new TicketSalesResponseModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }

            List<Ticket> results = await (await TicketRepo.GetAllAsync().ConfigureAwait(true))
              .Include(x => x.TicketSaleInfo).ThenInclude(x => x.TheatreSessionInfo)
             .ThenInclude(x => x.MovieInfo).Include(x => x.TicketSaleInfo.TheatreSessionInfo.TheatreInfo)
             .Include(x => x.TicketSaleInfo.TheatreSessionInfo.TheatreHallInfo)
             .Where(x => x.TicketSaleInfo.TheatreSessionInfo.Date == requestModel.Date.Date 
             && (requestModel.TheatreId < 1 || x.TicketSaleInfo.TheatreSessionInfo.TheatreId == requestModel.TheatreId)).ToListAsync().ConfigureAwait(true);

            return await GetFormattedResults(results).ConfigureAwait(true);
        }

        public async Task<TicketSalesResponseModel> TicketSalesRange(TicketSalesRangeModel requestModel)
        {
            if (requestModel == null)
            {
                return new TicketSalesResponseModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }

            List<Ticket> results = await (await TicketRepo.GetAllAsync().ConfigureAwait(true))
              .Include(x => x.TicketSaleInfo).ThenInclude(x => x.TheatreSessionInfo)
             .ThenInclude(x => x.MovieInfo).Include(x => x.TicketSaleInfo.TheatreSessionInfo.TheatreInfo)
             .Include(x => x.TicketSaleInfo.TheatreSessionInfo.TheatreHallInfo)
             .Where(x => x.TicketSaleInfo.TheatreSessionInfo.Date >= requestModel.StartDate.Date && x.TicketSaleInfo.TheatreSessionInfo.Date
           <= requestModel.EndDate.Date
             && x.TicketSaleInfo.TheatreSessionInfo.TheatreId == requestModel.TheatreId).ToListAsync().ConfigureAwait(true);

            return await GetFormattedResults(results).ConfigureAwait(true);
        }

        private async Task<TicketSalesResponseModel> GetFormattedResults(List<Ticket> results)
        {
            List<TicketSalesModel> sales = results
               .GroupBy(x => new { x.TicketSaleInfo.TheatreSessionInfo.TheatreId, x.TicketSaleInfo.TheatreSessionInfo.MovieId })
               .Select(x => new TicketSalesModel()
               {
                   MovieName = $@"{x.First().TicketSaleInfo.TheatreSessionInfo.MovieInfo.Name} ({LanguageConstants.GetString(x.First().TicketSaleInfo.TheatreSessionInfo.MovieInfo.Language)}) {((x.First().TicketSaleInfo.TheatreSessionInfo.MovieInfo.IsThreeD) ? "3D" : "2D")}",
                   Theatre = $"{x.First().TicketSaleInfo.TheatreSessionInfo.TheatreInfo?.Name} {x.First().TicketSaleInfo.TheatreSessionInfo.TheatreHallInfo?.Name}",
                   TotalSales = x.Sum(g => g.TicketPrice)
               }).ToList();

            return new TicketSalesResponseModel()
            {
                IsSuccess = true,
                Results = sales
            };
        }

        public async Task<TheatreSalesViewModel> GetSales()
        {
            List<SaleViewModel> sales = new List<SaleViewModel>();

            sales.Add(new SaleViewModel()
            {
                MovieName = "1917 (ENGLISH) 2D",
                ComplimentTickets = 0,
                BoxOffice = 98950,
                TotalBookedTicket = 138
            });

            sales.Add(new SaleViewModel()
            {
                MovieName = "BAD BOYS FOR LIFE (ENGLISH) 2D",
                ComplimentTickets = 0,
                BoxOffice = 87200,
                TotalBookedTicket = 93
            });

            sales.Add(new SaleViewModel()
            {
                MovieName = "BIRDS OF PREY (ENGLISH) 2D",
                ComplimentTickets = 0,
                BoxOffice = 68450,
                TotalBookedTicket = 89
            });

            sales.Add(new SaleViewModel()
            {
                MovieName = "LOVE AAJ KAL (HINDI) 2D",
                ComplimentTickets = 0,
                BoxOffice = 16900,
                TotalBookedTicket = 29
            });

            return new TheatreSalesViewModel()
            {
                IsSuccess = true,
                TheatreSales = sales,
                BoxOffice = sales.Sum(x => x.BoxOffice),
                ComplimentTickets = sales.Sum(x => x.ComplimentTickets),
                TotalBookedTicket = sales.Sum(x => x.ComplimentTickets),
                TotalTickets = sales.Sum(x => x.ComplimentTickets)
            };
        }
    }
}
