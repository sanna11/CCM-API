using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class TicketSale: BaseEntity
    {
        public int TotalSeats { get; set; }
        public double SoldAmount { get; set; }
        public int TheatreSessionId { get; set; }
        [ForeignKey("TheatreSessionId")]
        public TheatreSession TheatreSessionInfo { get; set; }
        public bool HasDiscount { get; set; }
        public double DiscountValue { get; set; }
        public DateTime IssuedTime { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
