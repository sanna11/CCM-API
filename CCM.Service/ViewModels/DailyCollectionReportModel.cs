using CCM.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class DailyCollectionReportModel : BaseResultViewModel
    {
        public DailyCollectionResult Result { get; set; }
    }

    public class DailyCollectionResult
    {
        public String MovieName { get; set; }
        public String Theatre { get; set; }
        public DateTime Date { get; set; }
        public double TotalSales { get; set; }
        public double Tax { get { return (TotalSales * 100.0) / GeneralConstant.TaxPerc; } }
        public IEnumerable<SessionSalesViewModel> Sales { get; set; }
    }
}
