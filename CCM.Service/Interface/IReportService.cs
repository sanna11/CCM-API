using CCM.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Service.Interface
{
    public interface IReportService
    {
        Task<TheatreSalesViewModel> GetSales();

        Task<TheatreSalesViewModel> GetBoxOfficeSummary(BoxOfficeRequestViewModel requestModel);

        Task<ConcessionSalesViewModel> ConcessionSalesReport(BoxOfficeRequestViewModel requestModel);
    }
}
