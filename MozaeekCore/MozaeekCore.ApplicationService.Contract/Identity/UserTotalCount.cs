using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Identity
{
    public class UserTotalCount
    {
        public long Count { get; private set; }
        public UserTotalCount(long count)
        {
            Count = count;
        }
    }
}
