using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class RssNewsChangeIsProcessCommand : Command
    {
        public long NewsId { get; set; }
    }
}