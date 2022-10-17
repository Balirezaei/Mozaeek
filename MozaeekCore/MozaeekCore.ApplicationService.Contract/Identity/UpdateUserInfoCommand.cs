using MozaeekCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract.Identity
{
    public class UpdateUserInfoCommand: Command
    {       
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string EMail { get; set; }
        public List<CoreRole> Roles { get; set; }
    }
}
