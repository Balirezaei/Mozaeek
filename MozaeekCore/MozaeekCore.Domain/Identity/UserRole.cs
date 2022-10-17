using MozaeekCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain.Identity
{
    public class UserRole
    {
        protected UserRole()
        {

        }
        public UserRole(CoreRole role)
        {            
            this.Role = role;
        }
        public UserRole(CoreRole role,long userId)
        {
            this.UserId = userId;
            this.Role = role;
        }
        public int Id { get; set; }
        public CoreRole Role { get; set; }        
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
