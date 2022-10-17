using System;
using Mozaeek.CR.PublicDto;

namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class UserVoiceRequestQuestionCreated: BaseEvent
    {
        public CommonQuestionInfo CommonQuestionInfo { get; set; }
        public UserInfo UserInfo { get; set; }
 
        public ProperPriceResult ProperPriceResult { get; set; }
        public VoiceInfo VoiceInfo { get; set; }
        public RequestQuestionInfo RequestQuestionInfo { get; set; }
        
        public UserVoiceRequestQuestionCreated(UserInfo userInfo, CommonQuestionInfo commonQuestionInfo, RequestQuestionInfo requestQuestionInfo, ProperPriceResult properPriceResult , VoiceInfo voiceInfo)
        {
            CommonQuestionInfo = commonQuestionInfo;
            UserInfo = userInfo;
            RequestQuestionInfo = requestQuestionInfo;
            ProperPriceResult = properPriceResult;
            VoiceInfo = voiceInfo;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}