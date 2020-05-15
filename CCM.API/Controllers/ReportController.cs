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

        [HttpGet("occupany-season")]
        public async Task<ActionResult<ConcessionSalesViewModel>> OccupancyBySeason([FromQuery] BoxOfficeRequestViewModel model)
        {
            return Ok(await Service.OccupancyPercentage(model).ConfigureAwait(true));
        }

        [HttpGet("daily-collection")]
        public async Task<ActionResult<DailyCollectionReportModel>> DailyCollection([FromQuery] DailyCollectionRequestViewModel model)
        {
            return Ok(await Service.DailyCollectionReport(model).ConfigureAwait(true));
        }

        [HttpGet("ticket-sales")]
        public async Task<ActionResult<DailyCollectionReportModel>> TicketSales([FromQuery] TicketSalesTodayRequestModel model)
        {
            return Ok(await Service.TicketSales(model).ConfigureAwait(true));
        }

        [HttpGet("ticket-sales-range")]
        public async Task<ActionResult<DailyCollectionReportModel>> TicketSalesRange([FromQuery] TicketSalesRangeModel model)
        {
            return Ok(await Service.TicketSalesRange(model).ConfigureAwait(true));
        }
    }
}