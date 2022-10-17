using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.BasicInfo.RequestTarget
{
    public class UserRequestTargetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CreateDateTime { get; set; }
    }
}
