using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class ContactPerson: BaseEntity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String MobileContactNo { get; set; }
        public String Designation { get; set; }
        public int TheatreId { get; set; }
        [ForeignKey("TheatreId")]
        public Theatre TheatreInfo { get; set; }
    }
}
