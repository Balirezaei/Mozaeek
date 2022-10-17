using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain
{
    public class TechnicianRequestTarget
    {
        public long Id { get; set; }
        public RequestTarget RequestTarget { get; set; }
        public long RequestTargetId { get; set; }

        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}
