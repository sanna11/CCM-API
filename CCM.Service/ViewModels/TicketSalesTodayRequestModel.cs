using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class TicketSalesTodayRequestModel
    {
        public int TheatreId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
