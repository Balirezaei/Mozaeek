using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class CreateAnnouncementCommand : Command
    {
        public CreateAnnouncementCommand()
        {
            Points = new List<long>();
            RequestOrgs = new List<long>();
            Subjects = new List<long>();
            Labels = new List<long>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public long NewsId { get; set; }
        public long? FileId { get; set; }
        public string Url { get; set; }
        public List<long> Points { get; set; }
        public AttachmentDto Photo { get; set; }
        public bool HasRequest { get; set; }
        public List<long> Subjects { get; set; }
        public List<long> Labels { get; set; }

        public List<long> RequestOrgs { get; set; }
    }

    // public class CreateRequestAnnouncementCommand : Command
    // {
    //     public CreateRequestAnnouncementCommand()
    //     {
    //         Points = new List<long>();
    //     }
    //
    //     public string Title { get; set; }
    //     public string Description { get; set; }
    //     public string Summary { get; set; }
    //     public long RequestTargetId { get; set; }
    //     public long NewsId { get; set; }
    //     public long? FileId { get; set; }
    //     public string Url { get; set; }
    //     public List<long> Points { get; set; }
    //     public AttachmentDto Photo { get; set; }
    //     public long? RequestId { get; set; }
    // }
}