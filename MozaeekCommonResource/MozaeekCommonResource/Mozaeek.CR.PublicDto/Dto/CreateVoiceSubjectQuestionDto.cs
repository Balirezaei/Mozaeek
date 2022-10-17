using System.Collections.Generic;

namespace Mozaeek.CR.PublicDto.Dto
{
    public class CreateVoiceSubjectQuestionDto
    {
        public long UserId { get; set; }
        public long SubjectId { get; set; }
        public ProperPriceResult ProperPrice { get; set; }
        public bool IsTextAnswer { get; set; }
        public string SubjectTitle { get; set; }
        public string QuestionTitle { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<QuestionAttachmentDto> QuestionAttachments { get; set; }
        public QuestionAttachmentDto Voice { get; set; }

        public CreateVoiceSubjectQuestionDto(long userId, string questionTitle, long subjectId, ProperPriceResult properPrice, bool isTextAnswer, string subjectTitle, TechnicianType technicianType, QuestionAttachmentDto voice, List<QuestionAttachmentDto> questionAttachments)
        {
            UserId = userId;
            SubjectId = subjectId;
            ProperPrice = properPrice;
            IsTextAnswer = isTextAnswer;
            SubjectTitle = subjectTitle;
            TechnicianType = technicianType;
            Voice = voice;
            QuestionAttachments = questionAttachments;
            QuestionTitle = questionTitle;
        }

        public CreateVoiceSubjectQuestionDto()
        {
        }
    }
}