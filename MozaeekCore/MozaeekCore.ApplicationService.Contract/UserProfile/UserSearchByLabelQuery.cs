using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract.UserProfile
{
    //public class UserSearchByLabelQuery
    //{
    //    public UserSearchByLabelQuery(long labelId, int pageNumber)
    //    {
    //        LabelId = labelId;
    //        PageNumber = pageNumber;
    //    }

    //    public long LabelId { get; set; }
    //    public int PageNumber { get; set; }
    //}

    public class UserSearchByRequestOrgQuery
    {
        public UserSearchByRequestOrgQuery(long subjectId, int pageNumber)
        {
            SubjectId = subjectId;
            PageNumber = pageNumber;
        }

        public long SubjectId { get; set; }
        public int PageNumber { get; set; }
    }

    public class UserSearchByRequestTargetQuery
    {
        public UserSearchByRequestTargetQuery(long requestTargetId, int pageNumber)
        {
            RequestTargetId = requestTargetId;
            PageNumber = pageNumber;
        }

        public long RequestTargetId { get; set; }
        public int PageNumber { get; set; }
    }

    public class UserSearchByBasicInfoQuery
    {
        public UserSearchByBasicInfoQuery(List<long> labelIds,List<long> subjectIds,List<long> requestOrgIds, int pageNumber)
        {
            LabelIds = labelIds;
            PageNumber = pageNumber;
            SubjectIds = subjectIds;
            RequestOrgIds = requestOrgIds;
            PageNumber = pageNumber;
        }

        public List<long> LabelIds { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<long> RequestOrgIds { get; set; }
        public int PageNumber { get; set; }
    }
}