using System.Collections.Generic;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class AnnouncementDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequestTargetTitle { get; set; }
        public List<long> Points { get; set; }
    }
}