using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CCM.Data.Models
{
    public class AuthUser : IdentityUser
    {
        public AuthUser(String username)
            : base(username)
        {
            if (this.InternalId == null)
            {
                this.InternalId = Guid.NewGuid();
            }
        }

        public AuthUser()
            : base()
        {
            if (this.InternalId == null)
            {
                this.InternalId = Guid.NewGuid();
            }
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Guid InternalId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
