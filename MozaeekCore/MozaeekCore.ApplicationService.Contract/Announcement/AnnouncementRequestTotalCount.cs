using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class AnnouncementRequestTotalCount : Command
    {
        public long Count { get; private set; }

        public AnnouncementRequestTotalCount(long count)
        {
            Count = count;
        }
    }
}