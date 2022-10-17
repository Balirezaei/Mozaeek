using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ViewModel
{
    public class TechnicianExpertViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNmuber { get; set; }
        public string NationalId { get; set; }
        public long PointId { get; set; }
        public List<long> FileIds { get; set; }
        public List<long> RequestTargetIds { get; set; }
        public List<long> DefiniteRequestOrgIds { get; set; }
    }
}
