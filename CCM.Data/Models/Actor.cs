using CCM.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Data.Models
{
    public class Actor :BaseEntity
    {
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public String ImageUrl { get; set; }
        public String FaceBookFanPage { get; set; }
    }
}
