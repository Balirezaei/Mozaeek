using System;
using System.Collections.Generic;
using Mozaeek.CR.PublicDto;

namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class UserVoiceSubjectQuestionCreated : BaseEvent
    {
        public UserInfo UserInfo { get; set; }
        public SubjectQuestionInfo SubjectQuestionInfo { get; set; }
        public ProperPriceResult ProperPriceResult { get; set; }
        public CommonQuestionInfo CommonQuestionInfo { get; set; }
        public VoiceInfo VoiceInfo { get; set; }

        public UserVoiceSubjectQuestionCreated(UserInfo userInfo, ProperPriceResult properPriceResult, VoiceInfo voiceInfo, CommonQuestionInfo commonQuestionInfo, SubjectQuestionInfo subjectQuestionInfo)
        {
            UserInfo = userInfo;
            ProperPriceResult = properPriceResult;
            VoiceInfo = voiceInfo;
            CommonQuestionInfo = commonQuestionInfo;
            SubjectQuestionInfo = subjectQuestionInfo;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}