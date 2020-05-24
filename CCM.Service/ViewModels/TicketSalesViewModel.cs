using CCM.Core.Constants;
using CCM.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TicketSalesViewModel
    {
        public String ClassName { get; set; }
        public double Rate { get; set; }
        public int Sold { get; set; }
        public double Amount { get; set; }
        public String TaxAmount { get { return ((Amount / 100.0) * GeneralConstant.TaxPerc).GetDecimalPoint(); } }
    }

    public class SessionSalesViewModel
    {
        public String SessionStart { get; set; }
        public String ClassName { get; set; }
        public double Rate { get { return Amount * 1.0 / Sold; } }
        public int Sold { get; set; }
        public double Amount { get; set; }
        public String TaxAmount { get { return ((Amount / 100.0) * GeneralConstant.TaxPerc).GetDecimalPoint(); } }
    }
}
