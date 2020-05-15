using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class Ticket : BaseEntity
    {
        public String TicketId { get; set; }
        public int TicketSaleId { get; set; }
        [ForeignKey("TicketSaleId")]
        public TicketSale TicketSaleInfo { get; set; }
        public TicketTypeEnum TicketType { get; set; }
        public double TicketPrice { get; set; }
    }
}
