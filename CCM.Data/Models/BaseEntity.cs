using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCM.Data.Models
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid InternalId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
