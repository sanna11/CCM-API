using CCM.Core.Constants;
using CCM.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class SaleViewModel
    {
        public String MovieName { get; set; }
        public int ComplimentTickets { get; set; }
        public int TotalBookedTicket { get; set; }
        public int TotalTickets { get { return this.TotalBookedTicket + this.ComplimentTickets; } }
        public double BoxOffice { get; set; }
        public double Tax { get { return this.BoxOffice / 100 * GeneralConstant.TaxPerc; } }
        public String TaxFormatted { get { return this.Tax.GetDecimalPoint(); } }
        public double NetIncome { get { return this.BoxOffice - this.Tax; } }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
