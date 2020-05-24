using CCM.Core.Constants;
using CCM.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public DateTime? Date { get; set; }
        public double TotalSales { get; set; }
        public String Tax { get { return ((TotalSales / 100.0) * GeneralConstant.TaxPerc).GetDecimalPoint(); } }
        public IEnumerable<SessionSalesViewModel> Sales { get; set; }
    }
}
