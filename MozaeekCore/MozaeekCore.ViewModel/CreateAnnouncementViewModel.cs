using System.Collections.Generic;

namespace MozaeekCore.ViewModel
{
    public class CreateAnnouncementViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public long NewsId { get; set; }
        public List<long> Points { get; set; }
        public List<long> Subjects { get; set; }
        public List<long> Labels { get; set; }

        public List<long> RequestOrgs { get; set; }
        public bool HasRequest { get; set; }
    }
}