using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TheatreSessionViewModel : BaseViewModel
    {
        public int MovieId { get; set; }
        public MovieViewModel MovieInfo { get; set; }
        public int? TheatreId { get; set; }
        public TheatreViewModel TheatreInfo { get; set; }
        public int? TheatrehallId { get; set; }
        public TheatreHallViewModel TheatreHallInfo { get; set; }
        public int? TotalTickets { get; set; }
        public int? ComplimentTickets { get; set; }
        public int? TotalBookedTicket { get; set; }
        public int? UsedTicket { get; set; }
        public double? Income { get; set; }
        public DateTime Date { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
    }
}
