using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class RssNewsChangeIsProcessCommand : Command
    {
        public long NewsId { get; set; }
    }
    

    public class AnnouncementRequestAssignRequestCommand : Command
    {
        public long RequestId { get; set; }
        public long AnnouncementId { get; set; }
    }
}