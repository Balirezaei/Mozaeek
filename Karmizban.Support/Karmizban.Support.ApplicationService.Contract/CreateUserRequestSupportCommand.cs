namespace Karmizban.Support.ApplicationService.Contract
{
    public class CreateUserRequestSupportCommand
    {
        public string Description { get; set; }
        public long QuestionId { get; set; }
        public string QuestionCode { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public long? UserSuggestedSupportId { get; set; }
    }
}