using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class Ticket: BaseEntity
    {
        public String TicketId { get; set; }
        public String Name { get; set; }
        public int TotalSeats { get; set; }
        public double SoldAmount { get; set; }
        public int SoldQuantity { get; set; }
        public int TheatreSessionId { get; set; }
        [ForeignKey("TheatreSessionId")]
        public TheatreSession TheatreSessionInfo { get; set; }
        public bool HasDiscount { get; set; }
        public TicketTypeEnum TicketType { get; set; }
        public DateTime IssuedTime { get; set; }
    }
}
