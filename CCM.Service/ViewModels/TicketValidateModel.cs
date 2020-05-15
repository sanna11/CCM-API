using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TicketValidateModel
    {
        [Required]
        public String TicketId { get; set; }
        public int TheatreId { get; set; }
        [Required]
        public int TheatreSessionId { get; set; }
        public DateTime Date { get; set; }
    }
}
