namespace Karmizban.Support.ApplicationService.Contract
{
    public class UserRequestSupportAnswerCommand
    {
        public string Description { get; set; }
        public long UserRequestSupportId { get; set; }
        public string Title { get; set; }
    }

    public class UserRequestSupportAnswerResult
    {

    }
}