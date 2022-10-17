using MozaeekCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain.Identity
{
    public class RoleNames
    {
        public const string Admin = nameof(Admin);
        public const string Operation = nameof(Operation);
        public const string BasiInfo = nameof(BasiInfo);
        public static string GetName(CoreRole role) =>
            role switch
            {
                CoreRole.Admin => Admin,
                CoreRole.Operation => Operation,
                CoreRole.BasiInfo => BasiInfo,
                _ => ""
            };
    }
}
