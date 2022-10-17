using System.Collections.Generic;

namespace Mozaeek.CR.PublicDto
{
    public class CreateTextSubjectQuestionDto
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public long SubjectId { get; set; }
        public ProperPriceResult ProperPrice { get; set; }
        public bool IsTextAnswer { get; set; }
        public string SubjectTitle { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<QuestionAttachmentDto> QuestionAttachments { get; set; }
        public string QuestionTitle { get; set; }

        public CreateTextSubjectQuestionDto(long userId, string questionTitle, string description, long subjectId, ProperPriceResult properPrice, bool isTextAnswer, string subjectTitle, TechnicianType technicianType, List<QuestionAttachmentDto> questionAttachments)
        {
            UserId = userId;
            Description = description;
            SubjectId = subjectId;
            ProperPrice = properPrice;
            IsTextAnswer = isTextAnswer;
            SubjectTitle = subjectTitle;
            TechnicianType = technicianType;
            QuestionAttachments = questionAttachments;
            QuestionTitle = questionTitle;
        }

        public CreateTextSubjectQuestionDto()
        {
        }
    }
}