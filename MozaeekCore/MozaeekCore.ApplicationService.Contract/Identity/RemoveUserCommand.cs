using MozaeekCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Identity
{
    public class RemoveUserCommand: Command
    {
        public long UserId { get; set; }
    }
}
