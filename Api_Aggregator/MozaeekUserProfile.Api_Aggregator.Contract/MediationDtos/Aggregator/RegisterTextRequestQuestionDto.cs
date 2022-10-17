using System.Collections.Generic;
using Mozaeek.CR.PublicDto;

namespace Api_Aggregator.Contract.MediationDtos.Aggregator
{
    public class RegisterTextRequestQuestionDto
    {
        public long UserId { get; set; }
        public long RequestId { get; set; }
        public string Description { get; set; }
        public bool IsTextAnswer { get; set; }
        public string QuestionTitle { get; set; }
        public List<QuestionAttachmentDto> Attachments { get; set; }
    }
}