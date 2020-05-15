using CCM.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Service.Interface
{
    public interface ITicketService : IBaseService<TicketViewModel>
    {
        Task<bool> SeedDataAsync();

        Task<BaseResultViewModel> ValidateTicket(TicketValidateModel validateModel);
    }
}
