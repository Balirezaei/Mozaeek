using System;
using System.Collections.Generic;
using Mozaeek.CR.PublicDto;

namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class CommonQuestionInfo
    {
        public Guid QuestionUniqId { get; set; }
        public string QuestionCode { get; set; }
        public long QuestionId { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public string QuestionTitle { get; set; }
        public UserQuestionState UserQuestionState { get; set; }
        public QuestionAnswerType AnswerType { get; set; }
        public List<QuestionAttachmentDto> QuestionAttachments { get; set; }

        public CommonQuestionInfo(Guid questionUniqId, string questionCode, long questionId, TechnicianType technicianType, string questionTitle, UserQuestionState userQuestionState, QuestionAnswerType answerType, List<QuestionAttachmentDto> questionAttachments)
        {
            QuestionUniqId = questionUniqId;
            QuestionCode = questionCode;
            QuestionId = questionId;
            TechnicianType = technicianType;
            QuestionTitle = questionTitle;
            UserQuestionState = userQuestionState;
            AnswerType = answerType;
            QuestionAttachments = questionAttachments;
        }
    }
}