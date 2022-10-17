using MozaeekCore.Core.Base;
using MozaeekCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Identity
{
    public class RegisterUserCommand: Command
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string EMail { get; set; }
        public List<CoreRole> Roles { get; set; }
    }
}
