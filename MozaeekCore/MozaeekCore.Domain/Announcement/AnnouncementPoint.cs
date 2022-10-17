using System.Collections;

namespace MozaeekCore.Domain
{
    public class AnnouncementPoint
    {
        public long Id { get; set; }
        public long PointId { get; set; }
        public virtual Point Point { get; set; }
        public long AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }
       
    }
}