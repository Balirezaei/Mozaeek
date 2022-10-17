using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class CreateAnnouncementCommand : Command
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long RequestTargetId { get; set; }
        public long NewsId { get; set; }
        public List<PointDto> Points { get; set; }
    }
}