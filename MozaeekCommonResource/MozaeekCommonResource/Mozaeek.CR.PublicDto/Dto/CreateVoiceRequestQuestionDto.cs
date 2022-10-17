using System.Collections.Generic;

namespace Mozaeek.CR.PublicDto
{
    public class CreateVoiceRequestQuestionDto
    {
        public long UserId { get; set; }
        public long RequestId { get; set; }
        public ProperPriceResult ProperPrice { get; set; }
        public bool IsTextAnswer { get; set; }
        public string RequestTitle { get; set; }
        public string QuestionTitle { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<QuestionAttachmentDto> QuestionAttachments { get; set; }
        public QuestionAttachmentDto Voice { get; set; }

        public CreateVoiceRequestQuestionDto(long userId, string questionTitle,  long requestId, ProperPriceResult properPrice, bool isTextAnswer, string requestTitle, TechnicianType technicianType, QuestionAttachmentDto voice, List<QuestionAttachmentDto> questionAttachments)
        {
            UserId = userId;
            RequestId = requestId;
            ProperPrice = properPrice;
            IsTextAnswer = isTextAnswer;
            RequestTitle = requestTitle;
            TechnicianType = technicianType;
            QuestionAttachments = questionAttachments;
            QuestionTitle = questionTitle;
            Voice = voice;
        }

        public CreateVoiceRequestQuestionDto()
        {
        }
    }
}