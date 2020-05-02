using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCM.Data.Models
{
    public class MovieCast : BaseEntity
    {
        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor ActorInfo { get; set; }
        public String Name { get; set; }
        public String RoleName { get; set; }
        public String RoleType { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie MovieInfo { get; set; }
    }
}
