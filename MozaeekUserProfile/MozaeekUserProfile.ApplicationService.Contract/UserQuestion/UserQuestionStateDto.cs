using Mozaeek.CR.PublicDto;

namespace MozaeekUserProfile.ApplicationService.Contract
{
    public class UpdateUserQuestionStateInput
    {
        public UserQuestionState State { get; set; }
        public long QuestionId { get; set; }
    }

    public class UpdateUserQuestionStateResult
    {
        public UserQuestionState State { get; set; }
        public long QuestionId { get; set; }
    }
}