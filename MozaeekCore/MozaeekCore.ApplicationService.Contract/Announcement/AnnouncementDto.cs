using System.Collections.Generic;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class AnnouncementDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<long> Points { get; set; }
        public List<RequestOrgDto> RequestOrgs { get; set; }
        public List<LabelDto> Labels { get; set; }
        public List<SubjectDto> Subjects { get; set; }
        public string ImagePath { get; set; }
        public string Summary { get; set; }
        public bool HasRequest { get; set; }
    }
}