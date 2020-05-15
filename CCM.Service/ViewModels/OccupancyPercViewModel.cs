using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class OccupancyPercViewModel
    {
        public String TheartreHalls { get; set; }
        public int TotalTickets { get; set; }
        public int UsedTickets { get; set; }
        public String StartTime { get; set; }
        public DateTime Date { get; set; }
        public String Day { get { return Date.ToString("ddd"); } }
        public double Percentage { get {  return ( UsedTickets * 100.0) / TotalTickets; } }
    }

    public class OccupancyPerMovie
    {
        public String MovieName { get; set; }
        public IEnumerable<OccupancyPercViewModel> OccupancyPercs { get; set; }
    }
}
