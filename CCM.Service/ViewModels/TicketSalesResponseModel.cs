using CCM.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TicketSalesResponseModel : BaseResultViewModel
    {
        public IEnumerable<TicketSalesModel> Results { get; set; }
    }

    public class TicketSalesModel
    {
        public String MovieName { get; set; }
        public String Theatre { get; set; }
        public double TotalSales { get; set; }
        public double Tax { get { return (TotalSales * 100.0) / GeneralConstant.TaxPerc; } }
    }
}
