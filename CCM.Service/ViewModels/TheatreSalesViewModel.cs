using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TheatreSalesViewModel: BaseResultViewModel
    {
        public List<SaleViewModel> TheatreSales { get; set; }

        public int TotalTickets { get; set; }
        public int ComplimentTickets { get; set; }
        public int TotalBookedTicket { get; set; }
        public int TotalUsedTicket { get { return this.TotalBookedTicket + this.ComplimentTickets; } }
        public double BoxOffice { get; set; }
        public double Tax { get { return this.BoxOffice * 0.1; } }
        public double NetIncome { get { return this.BoxOffice - this.Tax; } }
    }
}
