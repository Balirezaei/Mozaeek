using System.Collections.Generic;
using Mozaeek.CR.PublicDto;

namespace Api_Aggregator.Contract.MediationDtos.Aggregator
{
    public class RegisterVoiceSubjectQuestionDto
    {
        public long UserId { get; set; }
        public long SubjectId { get; set; }
        public QuestionAttachmentDto Voice { get; set; }
        public bool IsTextAnswer { get; set; }
        public string QuestionTitle { get; set; }
        public List<QuestionAttachmentDto> Attachments { get; set; }
    }
}