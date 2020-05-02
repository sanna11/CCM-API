using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class ConcessionSalesItemViewModel
    {
        public String MovieName { get; set; }
        public int Admits { get; set; }
        public int ConcessionTransactions { get; set; }
        public double ConcessionSales { get; set; }
        public double ConcessionCost { get; set; }
        public double ConcessionMargin { get { return ConcessionSales - ConcessionCost; } }
        public double BoxOffice { get; set; }
        public double TotalSaleAndConcession { get { return ConcessionSales + BoxOffice; } }
        public double AverageConcession { get { return ConcessionTransactions > 0 ? ConcessionSales / ConcessionTransactions : 0 ; } }
        public double TicketsPerPerson { get { return BoxOffice / Admits; } }
        public double ConcessionPerPerson { get { return ConcessionSales / Admits; } }
        public double TotalTickConcPerPerson { get { return TotalSaleAndConcession / Admits; } }
        public double PerConcesVsTick { get { return ConcessionSales / BoxOffice * 100; } }
        public double PerConcesVsAdmits { get { return ConcessionTransactions / Admits * 100; } }
    } 
}
