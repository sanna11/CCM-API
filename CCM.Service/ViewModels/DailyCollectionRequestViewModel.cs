using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCM.Service.ViewModels
{
    public class DailyCollectionRequestViewModel
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int TheatreId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
