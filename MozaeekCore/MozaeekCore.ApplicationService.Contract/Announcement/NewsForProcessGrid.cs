using System;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class NewsForProcessGrid
    {
        public long Id { get; set; }
        // public DateTime ModifiedDate { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
        public string Link { get; set; }
        public string CreateDate { get; set; }
        public string Source { get; set; }
    }



    public class NewsForProcess
    {
        public long Id { get; set; }
        // public DateTime ModifiedDate { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
        public string Link { get; set; }
        public DateTime CreateDate { get; set; }
        public string Source { get; set; }
    }
}