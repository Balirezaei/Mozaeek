using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestTargetCommandResult
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public List<LabelDto> LabelDtos { get; set; }
        public List<RequestOrgDto> RequestOrgDtos { get; set; }
        public List<SubjectDto> SubjectDtos { get; set; }

    }
}