using MozaeekCore.Core.Base;
using MozaeekCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Identity
{
    public class UnAssignRoleToUserCommand: Command
    {
        public long UserId { get; set; }
        public CoreRole Role { get; set; }
    }
}
