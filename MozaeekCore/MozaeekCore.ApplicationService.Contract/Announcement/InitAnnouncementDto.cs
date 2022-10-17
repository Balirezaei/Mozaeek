using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class InitAnnouncementDto
    {
        public List<PointDto> Points { get; set; }
        public List<LabelDto> Labels { get; set; }
        public List<RequestOrgDto> RequestOrgs { get; set; }
        public List<SubjectDto> Subjects { get; set; }
    }
}