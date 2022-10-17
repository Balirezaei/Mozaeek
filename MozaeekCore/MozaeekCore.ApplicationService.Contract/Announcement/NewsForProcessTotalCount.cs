using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class NewsForProcessTotalCount : Command
    {
        public long Count { get; private set; }

        public NewsForProcessTotalCount(long count)
        {
            Count = count;
        }
    }
}