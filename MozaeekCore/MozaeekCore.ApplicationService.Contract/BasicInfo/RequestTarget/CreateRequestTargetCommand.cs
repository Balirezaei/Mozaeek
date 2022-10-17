using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestTargetCommand : Command
    {
        public CreateRequestTargetCommand()
        {
            //RequestOrgs = new List<RequestOrgDto>();
            Subjects = new List<SubjectDto>();
            Labels = new List<LabelDto>();
        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public bool IsDocument { get; set; }

        public List<SubjectDto> Subjects { get; set; }
        public List<LabelDto> Labels { get; set; }

        //public List<RequestOrgDto> RequestOrgs { get; set; }

    }
}