using MozaeekCore.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class AnnouncementUserDashboardPagingContract: PagingContract
    {
        public long UserId { get; set; }
      //  public List<long> LabelIds { get; set; }
        public List<long> SubjectIds { get; set; }
        //public List<long> RequestOrgIds { get; set; }
        //public long PointId { get; set; }
    }
}
