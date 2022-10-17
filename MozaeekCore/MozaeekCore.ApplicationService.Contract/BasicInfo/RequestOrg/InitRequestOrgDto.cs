using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitRequestOrgDto
    {
        public List<RequestOrgDto> RequestOrgs { get; set; }
    }

    public class InitDefiniteRequestOrg
    {
        public List<PointDto> Points { get; set; }
    }
}