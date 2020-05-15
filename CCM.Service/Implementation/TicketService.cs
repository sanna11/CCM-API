using CCM.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using CCM.Service.ViewModels;
using CCM.Data.Models;
using CCM.Data.Repository;
using AutoMapper;
using CCM.Data.UOW;
using System.Threading.Tasks;
using CCM.Core.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CCM.Service.Implementation
{
    public class TicketService: BaseService<TicketViewModel, TicketSale>, ITicketService
    {
        private IBaseRepository<TicketSale> TicketSaleRepo { get; set; }
        private IBaseRepository<Ticket> TicketRepo { get; set; }

        public TicketService(IBaseRepository<TicketSale> repo, IBaseRepository<Ticket> ticketRepo,
            IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow, repo)
        {
            this.TicketSaleRepo = repo;
            this.TicketRepo = ticketRepo;
        }

        public async Task<BaseResultViewModel> ValidateTicket(TicketValidateModel validateModel)
        {
            if(validateModel == null)
            {
                return new BaseResultViewModel()
                {
                    ErrorMessage = "Missing request params"
                };
            }

            Ticket ticket = await (await TicketRepo.GetAllAsync().ConfigureAwait(true))
              .Include(x => x.TicketSaleInfo).ThenInclude(x=> x.TheatreSessionInfo)
              .Where(x=> x.TicketId.Equals(validateModel.TicketId) && x.TicketSaleInfo.TheatreSessionId == validateModel.TheatreSessionId)
              .FirstOrDefaultAsync().ConfigureAwait(true);

            return new BaseResultViewModel()
            {
                IsSuccess = ticket != null,
                ErrorMessage = ticket == null ? "Ticket is not available" : ""
            };
        }

        public async Task<bool> SeedDataAsync()
        {
            List<TicketSale> ticketSales = new List<TicketSale>();

            ticketSales.Add(new TicketSale()
            {
                IssuedTime = new DateTime(2020, 02, 01, 18, 35, 11),
                SoldAmount = 2350,
                TheatreSessionId = 2,
                TotalSeats = 5,
                Tickets = new List<Ticket>() {
                    new Ticket()
                    {
                        TicketId = "CN200201ODF1",
                        TicketPrice = 550,
                        TicketType = TicketTypeEnum.ODC_Full
                    },
                    new Ticket()
                    {
                        TicketId = "CN200201ODF2",
                        TicketPrice = 550,
                        TicketType = TicketTypeEnum.ODC_Full
                    },
                    new Ticket()
                    {
                        TicketId = "CN200201ODF3",
                        TicketPrice = 550,
                        TicketType = TicketTypeEnum.ODC_Full
                    },
                    new Ticket()
                    {
                        TicketId = "CN200201ODF4",
                        TicketPrice = 350,
                        TicketType = TicketTypeEnum.ODC_Half
                    },
                    new Ticket()
                    {
                        TicketId = "CN200201ODF5",
                        TicketPrice = 350,
                        TicketType = TicketTypeEnum.ODC_Half
                    }
                }
            });

            await TicketSaleRepo.CreateRangeAsync(ticketSales).ConfigureAwait(true);
            await UOW.Commit().ConfigureAwait(true);
            return true;
        }
    }
}
