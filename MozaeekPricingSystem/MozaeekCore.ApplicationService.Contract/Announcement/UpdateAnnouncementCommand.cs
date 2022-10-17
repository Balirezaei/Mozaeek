using System.Collections.Generic;
using MozaeekCore.Core.Base;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class UpdateAnnouncementCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequestTargetTitle { get; set; }
        public List<LabelDto> RequestTargetLabels { get; set; }
        public List<RequestOrgDto> RequestTargetRequestOrgs { get; set; }
        public List<SubjectDto> RequestTargetSubjects { get; set; }
        public List<Point> Points { get; set; }
    }
}