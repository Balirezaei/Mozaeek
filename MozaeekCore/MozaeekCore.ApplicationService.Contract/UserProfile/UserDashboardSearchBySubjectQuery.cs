namespace MozaeekCore.ApplicationService.Contract.UserProfile
{
    public class UserDashboardSearchBySubjectQuery
    {
        public UserDashboardSearchBySubjectQuery(long[] subjectIds, int pageNumber)
        {
            SubjectIds = subjectIds;
            PageNumber = pageNumber;
        }

        public long[] SubjectIds { get; set; }
        public int PageNumber { get; set; }
    }
}