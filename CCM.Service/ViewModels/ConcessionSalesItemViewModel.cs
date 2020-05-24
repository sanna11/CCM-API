using CCM.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public String AverageConcession { get { return ConcessionTransactions > 0 ? (ConcessionSales / ConcessionTransactions).GetDecimalPoint() : "0" ; } }
        public String TicketsPerPerson { get { return (BoxOffice / Admits).GetDecimalPoint(); } }
        public String ConcessionPerPerson { get { return (ConcessionSales / Admits).GetDecimalPoint(); } }
        public String TotalTickConcPerPerson { get { return (TotalSaleAndConcession / Admits).GetDecimalPoint(); } }
        public String PerConcesVsTick { get { return (ConcessionSales / 100.0 * BoxOffice).GetDecimalPoint(); } }
        public String PerConcesVsAdmits { get { return (ConcessionTransactions / 100.0 * Admits).GetDecimalPoint(); } }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    } 
}
