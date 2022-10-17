using System.Collections.Generic;

namespace Mozaeek.CR.PublicDto
{
    public class CreateTextRequestQuestionDto
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public long RequestId { get; set; }
        public ProperPriceResult ProperPrice { get; set; }
        public bool IsTextAnswer { get; set; }
        public string RequestTitle { get; set; }
        public string QuestionTitle { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<QuestionAttachmentDto> QuestionAttachments { get; set; }

        public CreateTextRequestQuestionDto(long userId, string questionTitle, string description, long requestId, ProperPriceResult properPrice, bool isTextAnswer, string requestTitle, TechnicianType technicianType, List<QuestionAttachmentDto> questionAttachments)
        {
            UserId = userId;
            Description = description;
            RequestId = requestId;
            ProperPrice = properPrice;
            IsTextAnswer = isTextAnswer;
            RequestTitle = requestTitle;
            TechnicianType = technicianType;
            QuestionAttachments = questionAttachments;

            QuestionTitle = questionTitle;
        }

        public CreateTextRequestQuestionDto()
        {
        }
    }
}