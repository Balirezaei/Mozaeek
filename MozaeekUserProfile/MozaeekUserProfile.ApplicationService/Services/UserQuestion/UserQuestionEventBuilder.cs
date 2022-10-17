using System;
using System.Collections.Generic;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Dto;
using Mozaeek.CR.PublicDto.Enum;
using Mozaeek.CR.PublicEvent.UserProfile;
using MozaeekUserProfile.Domain;
using Newtonsoft.Json;

namespace MozaeekUserProfile.ApplicationService.Services
{
    public class UserQuestionEventBuilder
    {
        private readonly UserQuestion _question;
        private readonly List<QuestionAttachmentDto> _attachments;
        private readonly User _user;
        private readonly ProperPriceResult _properPrice;

        public UserQuestionEventBuilder(UserQuestion question, List<QuestionAttachmentDto> attachments, User user, ProperPriceResult properPrice)
        {
            _question = question;
            _attachments = attachments;
            _user = user;
            _properPrice = properPrice;
        }

        public UserQuestionEventBuilder(UserQuestion question, User user)
        {
            _question = question;
            _user = user;
        }

        private CommonQuestionInfo _commonQuestionInfo
        {
            get
            {
                return BuildCommonQuestionInfo();
            }
        }
        private UserInfo _userInfo
        {
            get
            {
                return BuildUserInfo();
            }
        }

        private RequestQuestionInfo _RequestQuestionInfo
        {
            get
            {
                return BuildRequestQuestionInfo();
            }
        }
        private SubjectQuestionInfo _SubjectQuestionInfo
        {
            get
            {
                return BuildSubjectQuestionInfo();
            }
        }


        private VoiceInfo _voiceInfo
        {
            get
            {
                return BuildVoiceInfo();
            }

        }
        public UserTextRequestQuestionCreated BuildTextRequest()
        {
            return new UserTextRequestQuestionCreated(_userInfo, _properPrice, _question.TextDescription, _RequestQuestionInfo, _commonQuestionInfo);
        }

        public UserVoiceRequestQuestionCreated BuildVoiceRequest()
        {
            return new UserVoiceRequestQuestionCreated(_userInfo, _commonQuestionInfo, _RequestQuestionInfo, _properPrice, _voiceInfo);
        }

        public UserTextSubjectQuestionCreated BuildTextSubject()
        {
            return new UserTextSubjectQuestionCreated(_userInfo, _properPrice, _question.TextDescription, _commonQuestionInfo, _SubjectQuestionInfo);
        }

        public UserVoiceSubjectQuestionCreated BuildVoiceSubject()
        {
            return new UserVoiceSubjectQuestionCreated(_userInfo, _properPrice, _voiceInfo, _commonQuestionInfo, _SubjectQuestionInfo);
        }

        private CommonQuestionInfo BuildCommonQuestionInfo()
        {
            return
                new CommonQuestionInfo(_question.QuestionUniqId, _question.QuestionCode(), _question.Id,
                    _question.TechnicianType,
                    _question.QuestionTitle, UserQuestionState.SendByUser, _question.AnswerType, _attachments);
        }

        private UserInfo BuildUserInfo()
        {
            return new UserInfo(_user.Id, _user.FullName(), _user.DeviceId);
        }

        private RequestQuestionInfo BuildRequestQuestionInfo()
        {
            return new RequestQuestionInfo(_question.RequestId.Value, _question.RequestTitle);
        }

        private VoiceInfo BuildVoiceInfo()
        {
            return new VoiceInfo(_question.VoiceHttpPath, _question.VoiceFileId.ToString());
        }
        private SubjectQuestionInfo BuildSubjectQuestionInfo()
        {
            return new SubjectQuestionInfo(_question.SubjectId.Value, _question.SubjectTitle);
        }

        public PushNotificationMessage BuildNotificationEventByState(UserQuestionState state)
        {
            if (state==UserQuestionState.TechnicianNotFound)
            {
                var j_object = new JsonNotifOperation
                {
                    Type = JsonNotifOperationType.UserQuestionResponse,
                    Detail = new UserQuestionResponse
                    {
                        QuestionId = this._question.Id.ToString(),
                        Response = "مشاوران درحال پاسخگویی به سایر کاربران هستند، با فعال کردن سرویس خبردار، پرسش شما پس از آمادگی مشاوران، پاسخ داده می شود.",
                        TechnicianIsFound = false,
                    }

                }; 
                return new PushNotificationMessage()
                {
                    Title = "Technician Not Found",
                    Type = PushNotificationType.JsonMessage,
                    Payload = JsonConvert.SerializeObject(j_object),
                    Content = "",
                    ReceiverId = this._user.DeviceId,
                    PublishDateTime = DateTime.Now,
                    EventId = Guid.NewGuid()
                };
            }
            else
            {
                return null;
            }
         
        }

    }
}