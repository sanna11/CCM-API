using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CCM.Data.Models
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        Guid InternalId { get; set; }
        DateTime? CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
        [DefaultValue(false)]
        bool IsDeleted { get; set; }
    }
}
