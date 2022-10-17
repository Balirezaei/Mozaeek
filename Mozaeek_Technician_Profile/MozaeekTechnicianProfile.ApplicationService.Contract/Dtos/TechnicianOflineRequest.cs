using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class TechnicianOflineRequest
    {
        public long Id { get;  set; }
        public long RequestId { get;  set; }
        public string RequestTitle { get;  set; }

    }
}
