using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class InitAnnouncementDto
    {
        public List<PointDto> Points { get; set; }
        public List<SmallRequestTargetDto> RequestTargets { get; set; }
    }
}