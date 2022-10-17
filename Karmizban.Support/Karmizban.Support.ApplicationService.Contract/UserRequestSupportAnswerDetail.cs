namespace Karmizban.Support.ApplicationService.Contract
{
    public class UserRequestSupportAnswerDetail
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
    }

    public class GetUserRequestSupportAnswerContract
    {
        public long AnswerId { get; set; }
        public long UserId { get; set; }
    }
}