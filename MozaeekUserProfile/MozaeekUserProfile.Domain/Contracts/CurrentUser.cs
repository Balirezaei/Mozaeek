using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekUserProfile.Domain.Contracts
{
    public class CurrentUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
