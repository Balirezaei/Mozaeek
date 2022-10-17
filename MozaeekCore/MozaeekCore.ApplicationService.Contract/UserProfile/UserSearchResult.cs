using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.ApplicationService.Contract.BasicInfo.RequestTarget;
using MozaeekCore.Core.ResponseMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.UserProfile
{
    public class UserSearchResult
    {
        public PagedListResult<UserAnnouncementDto> Announcments { get; set; }
        public PagedListResult<RequestTargetMobileView> RequestTargets { get; set; }
    }
}
