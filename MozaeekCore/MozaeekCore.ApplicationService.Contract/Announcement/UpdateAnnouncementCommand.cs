using System.Collections.Generic;
using MozaeekCore.Core.Base;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class UpdateAnnouncementCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<long> Subjects { get; set; }
        public List<long> Labels { get; set; }
        public List<long> RequestOrgs { get; set; }
        public List<long> Points { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public long FileId { get; set; }
        public bool HasRequest { get; set; }
        public long? RequestId { get; set; }
    }
}