using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class AnnouncementGrid
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string RequestTargetTitle { get; set; }
        public List<string> RequestTargetLabels { get; set; }
        public List<string> RequestTargetRequestOrgs { get; set; }
        public List<string> RequestTargetSubjects { get; set; }
        public List<string> Points { get; set; }
        public string PublishDate { get; set; }
    }
}