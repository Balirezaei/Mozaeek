namespace MozaeekUserProfile.ApplicationService.Contract
{
    public class UserQuestionHistory
    {
        public string QuestionCode { get; set; }
        public string QuestionSubject { get; set; }
        public long QuestionId { get; set; }
        public string CreateDate { get; set; }
        public string TechnicianCode { get; set; }
        public string QuestionStateDescription { get; set; }
    }

    public class ActiveUserQuestion
    {
        public int RecentlyReceivedAnswer { get; set; }
        public int WaitingForAnswer { get; set; }
    }

    public class OpenUserQuestion
    {
        public long QuestionId { get; set; }
        public string QuestionTitle { get; set; }
    }
}