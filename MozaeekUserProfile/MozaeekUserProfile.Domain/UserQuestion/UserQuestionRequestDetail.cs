namespace MozaeekUserProfile.Domain
{
    public class UserQuestionRequestDetail
    {
        public UserQuestionRequestDetail(string requestTitle, long requestId)
        {
            RequestTitle = requestTitle;
            RequestId = requestId;
        }

        public string RequestTitle { get; set; }
        public long RequestId { get; set; }
    }
}