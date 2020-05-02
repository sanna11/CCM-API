using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class TheatreHall: BaseEntity
    {
        public String Name { get; set; }
        public int TotalSeatingCapacity { get; set; }
        public int TheatreId { get; set; }
        [ForeignKey("TheatreId")]
        public Theatre TheatreInfo { get; set; }
    }
}
