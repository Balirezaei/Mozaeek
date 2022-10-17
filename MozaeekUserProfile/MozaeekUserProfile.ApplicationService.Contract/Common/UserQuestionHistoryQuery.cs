namespace MozaeekUserProfile.ApplicationService.Contract
{
    public class UserQuestionHistoryQuery
    {
        public long UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}