using System;
using System.Collections.Generic;
using Mozaeek.CR.PublicDto;

namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class UserTextSubjectQuestionCreated : BaseEvent
    {
        public CommonQuestionInfo CommonQuestionInfo { get; set; }
        public UserInfo UserInfo { get; set; }
        public SubjectQuestionInfo SubjectQuestionInfo { get; set; }
        public ProperPriceResult ProperPriceResult { get; set; }
        public string QuestionTextDescription { get; set; }
        public UserTextSubjectQuestionCreated(UserInfo userInfo, ProperPriceResult properPriceResult, string questionTextDescription, CommonQuestionInfo commonQuestionInfo, SubjectQuestionInfo subjectQuestionInfo)
        {
            UserInfo = userInfo;
            ProperPriceResult = properPriceResult;
            QuestionTextDescription = questionTextDescription;
            CommonQuestionInfo = commonQuestionInfo;
            SubjectQuestionInfo = subjectQuestionInfo;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}