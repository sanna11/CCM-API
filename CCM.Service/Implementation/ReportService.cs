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

namespace CCM.Service.Implementation
{
    public class ReportService :IReportService
    {
        protected IMapper Mapper { get; set; }
        protected IUnitOfWork UOW { get; set; }
        protected IBaseRepository<TheatreSession> Repo { get; set; }

        public ReportService(IMapper mapper, IUnitOfWork uow, IBaseRepository<TheatreSession> repo)
        {
            this.Mapper = mapper;
            this.Repo = repo;
            this.UOW = uow;
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
