using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class TheatreSession : BaseEntity
    {
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie MovieInfo { get; set; }
        public int? TheatreId { get; set; }
        [ForeignKey("TheatreId")]
        public Theatre TheatreInfo { get; set; }
        public int?  TheatrehallId { get; set; }
        [ForeignKey("TheatrehallId")]
        public TheatreHall TheatreHallInfo { get; set; }
        public int? TotalTickets { get; set; }
        public int? ComplimentTickets { get; set; }
        public int? TotalBookedTicket { get; set; }
        public int? UsedTicket { get; set; }
        public int ConcessTrans { get; set; }
        public double ConcessSales { get; set; }
        public double ConcessCost { get; set; }
        public double? Income { get; set; }
        public DateTime Date { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
    }
}
