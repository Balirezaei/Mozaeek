using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class UserAnnouncementDto
    {
        public long Id { get; set; }
        public string Title { get;set; }
        public string CreateDateTime { get; set; }
        public string  PictureUrl{ get; set; }
        // public string PictureName { get; set; }
    }
    //ToDo : Move to Common Resource
    public class SubjectWithPriceDetailDto
    {
        public long SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int PriceAmount { get; set; }
        public int TechnicianCount { get; set; }
    }
}
