namespace MozaeekUserProfile.ApplicationService.Contract
{
    public class CancelQuestionInput
    {
        public long UserId { get; set; }
        public long QuestionId { get; set; }
    }
}