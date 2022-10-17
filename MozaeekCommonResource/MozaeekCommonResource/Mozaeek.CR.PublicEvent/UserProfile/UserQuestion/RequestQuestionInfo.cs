namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class RequestQuestionInfo
    {
        public long RequestId { get; set; }
        public string RequestTitle { get; set; }

        public RequestQuestionInfo(long requestId, string requestTitle)
        {
            RequestId = requestId;
            RequestTitle = requestTitle;
        }
    }
}