using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestTargetCommand : Command
    {
        public string Title { get; set; }
        public List<SubjectDto> Subjects { get; set; }
        public List<LabelDto> Labels { get; set; }

        public List<RequestOrgDto> RequestOrgs { get; set; }

    }
}