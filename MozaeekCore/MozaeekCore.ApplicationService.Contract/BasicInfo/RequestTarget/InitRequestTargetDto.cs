using System.Collections.Generic;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitRequestTargetDto
    {
        public List<LabelDto> Labels { get; set; }
        public List<RequestOrgDto> RequestOrgs { get; set; }
        public List<SubjectDto> Subjects { get; set; }

    }
}