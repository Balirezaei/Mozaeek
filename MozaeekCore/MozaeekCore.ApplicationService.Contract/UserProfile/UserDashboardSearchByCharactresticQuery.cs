using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract.UserProfile
{
    public class UserDashboardSearchByCharactresticQuery
    {
        public UserDashboardSearchByCharactresticQuery(List<long> labelIds, int pageNumber)
        {
            LabelIds = labelIds;
            PageNumber = pageNumber;
        }

        public List<long> LabelIds { get; set; }
        public int PageNumber { get; set; }
    }
}