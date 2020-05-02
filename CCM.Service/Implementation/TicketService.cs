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

namespace CCM.Service.Implementation
{
    public class TicketService: BaseService<TicketViewModel, Ticket>, ITicketService
    {
        private IBaseRepository<Ticket> TicketRepo { get; set; }

        public TicketService(IBaseRepository<Ticket> repo,
            IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow, repo)
        {
            this.TicketRepo = repo;
        }

        public async Task<bool> SeedDataAsync()
        {


            throw new NotImplementedException();
        }
    }
}
