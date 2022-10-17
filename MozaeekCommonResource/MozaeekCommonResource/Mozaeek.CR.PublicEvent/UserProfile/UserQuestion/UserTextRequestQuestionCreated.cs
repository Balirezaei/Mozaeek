using System;
using System.Collections.Generic;
using Mozaeek.CR.PublicDto;

namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class UserTextRequestQuestionCreated : BaseEvent
    {
        public UserInfo UserInfo { get; set; }

        public RequestQuestionInfo RequestQuestionInfo { get; set; }
        public ProperPriceResult ProperPriceResult { get; set; }
        public string QuestionTextDescription { get; set; }
        public CommonQuestionInfo CommonQuestionInfo { get; set; }

        public UserTextRequestQuestionCreated(UserInfo userInfo, ProperPriceResult properPriceResult, string questionTextDescription, RequestQuestionInfo requestQuestionInfo, CommonQuestionInfo commonQuestionInfo)
        {
            UserInfo = userInfo;
            ProperPriceResult = properPriceResult;
            QuestionTextDescription = questionTextDescription;
            RequestQuestionInfo = requestQuestionInfo;
            CommonQuestionInfo = commonQuestionInfo;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}