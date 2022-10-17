using System;
using System.Threading.Tasks;
using MassTransit;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Dto;
using Mozaeek.CR.PublicDto.Enum;
using Mozaeek.CR.PublicEvent.UserProfile;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.TechnicianProfileConsistensyService.Model;
using MozaeekCore.TechnicianProfileConsistensyService.Service;
using Newtonsoft.Json;

namespace MozaeekCore.TechnicianProfileConsistensyService.Consumer
{
    public class UserVoiceRequestQuestionCreatedConsumer : IConsumer<UserVoiceRequestQuestionCreated>
    {
        private readonly IOutboxRepository _outboxRepository;
        private readonly ITechnicianUserQuestionService _technicianUserQuestionService;
        public UserVoiceRequestQuestionCreatedConsumer(IOutboxRepository outboxRepository
            , ITechnicianUserQuestionService technicianUserQuestionService
        )
        {
            _outboxRepository = outboxRepository;
            _technicianUserQuestionService = technicianUserQuestionService;
        }

        public async Task Consume(ConsumeContext<UserVoiceRequestQuestionCreated> context)
        {

            Console.WriteLine($"{JsonConvert.SerializeObject(context.Message)}");

            await _technicianUserQuestionService.CreateTechnicianUserQuestion(new UserQuestionWaitingForTechnician()
            {
                TechnicianType = context.Message.CommonQuestionInfo.TechnicianType,
                UnitPrice = context.Message.ProperPriceResult.UnitPrice,
                AnswerType = context.Message.CommonQuestionInfo.AnswerType,
                PriceCurrencyType = (PriceCurrencyType)context.Message.ProperPriceResult.PriceCurrencyId,
                QuestionCode = context.Message.CommonQuestionInfo.QuestionCode,
                QuestionTitle = context.Message.CommonQuestionInfo.QuestionTitle,
                QuestionId = context.Message.CommonQuestionInfo.QuestionId,
                VoiceHttpPath = context.Message.VoiceInfo.VoiceHttpPath,
                VoiceFileId = long.Parse(context.Message.VoiceInfo.VoiceFileId),
                QuestionType = QuestionAnswerType.Text,
                SystemPriceShare = context.Message.ProperPriceResult.SystemShare,
                TechnicianPriceShare = context.Message.ProperPriceResult.TechnicianShare,
                UserFullName = context.Message.UserInfo.UserFullName,
                UserDeviceId = context.Message.UserInfo.UserDeviceId,
                UserId = context.Message.UserInfo.UserId,
                UserQuestionState = UserQuestionState.SendByUser,
                RequestTitle = context.Message.RequestQuestionInfo.RequestTitle,
                RequestId = context.Message.RequestQuestionInfo.RequestId,
            });

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);

            //Console.WriteLine($"{JsonConvert.SerializeObject(context.Message)}");
            //var j_object = new JsonNotifOperation
            //{
            //    Type = JsonNotifOperationType.UserQuestionResponse,
            //    Detail = new UserQuestionResponse
            //    {
            //        QuestionId = context.Message.CommonQuestionInfo.QuestionId.ToString(),
            //        Response =
            //            "مشاوران درحال پاسخگویی به سایر کاربران هستند، با فعال کردن سرویس خبردار، پرسش شما پس از آمادگی مشاوران، پاسخ داده می شود.",
            //        TechnicianIsFound = false,
            //    }

            //};
            //var pushnotif = new PushNotificationMessage()
            //{
            //    Title = "Technician Not Found",
            //    Type = PushNotificationType.JsonMessage,
            //    Payload = JsonConvert.SerializeObject(j_object),
            //    Content = "",
            //    ReceiverId = context.Message.UserInfo.UserDeviceId,
            //    PublishDateTime = DateTime.Now,
            //    EventId = Guid.NewGuid()
            //};
            //_outboxRepository.CreateOutboxMessage(new OutboxMessage(pushnotif, pushnotif.EventId, pushnotif.PublishDateTime));
            //await _outboxRepository.SaveChange();
        }
    }
}