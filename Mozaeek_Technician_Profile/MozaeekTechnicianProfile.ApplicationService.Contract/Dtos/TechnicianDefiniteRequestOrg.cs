using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class TechnicianDefiniteRequestOrg
    {
        public long Id { get;  set; }
        public long RequestOrgId { get;  set; }
        public string RequestOrgTitle { get;  set; }
    }
}
