using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TicketViewModel: BaseViewModel
    {
        public String TicketId { get; set; }
        public String Name { get; set; }
        public int TotalSeats { get; set; }
        public double SoldAmount { get; set; }
        public int SoldQuantity { get; set; }
        public int TheatreSessionId { get; set; }
        public TheatreSessionViewModel TheatreSessionInfo { get; set; }
        public bool HasDiscount { get; set; }
        public TicketTypeEnum TicketType { get; set; }
        public DateTime IssuedTime { get; set; }
    }
}
