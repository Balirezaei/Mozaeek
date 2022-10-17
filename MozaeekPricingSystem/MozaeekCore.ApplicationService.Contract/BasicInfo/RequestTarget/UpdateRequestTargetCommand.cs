using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateRequestTargetCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<SubjectDto> Subjects { get; set; }
        public List<LabelDto> Labels { get; set; }
        public List<RequestOrgDto> RequestOrgs { get; set; }
    }
}