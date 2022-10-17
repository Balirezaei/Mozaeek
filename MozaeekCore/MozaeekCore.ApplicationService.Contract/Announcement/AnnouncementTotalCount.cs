using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class AnnouncementTotalCount : Command
    {
        public long Count { get; private set; }

        public AnnouncementTotalCount(long count)
        {
            Count = count;
        }
    }

}