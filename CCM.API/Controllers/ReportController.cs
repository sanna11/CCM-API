using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCM.Service.Interface;
using CCM.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCM.API.Controllers
{
    public class ReportController : BaseAPIController
    {
        protected IReportService Service { get; set; } 

        public ReportController(IReportService service)
        {
            this.Service = service;
        }

        [HttpGet("sales")]
        public async Task<ActionResult<TheatreSalesViewModel>> GetSalesByDate()
        {
            return Ok(Service.GetSales());
        }

        [HttpGet("box-office")]
        public async Task<ActionResult<TheatreSalesViewModel>> GetSalesByDate([FromQuery] BoxOfficeRequestViewModel model)
        {
            return Ok(await Service.GetBoxOfficeSummary(model).ConfigureAwait(true));
        }

        [HttpGet("concession-sales")]
        public async Task<ActionResult<ConcessionSalesViewModel>> ConcessionSalesByDate([FromQuery] BoxOfficeRequestViewModel model)
        {
            return Ok(await Service.ConcessionSalesReport(model).ConfigureAwait(true));
        }
    }
}