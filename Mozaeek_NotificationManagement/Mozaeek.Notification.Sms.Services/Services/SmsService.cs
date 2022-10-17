using Mozaeek.Notification.Domain.Dtos;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Domain.Models;
using Mozaeek.Notification.Sms.Services.SmsProviders;
using Mozaeek.Notification.Sms.Services.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.Services
{
    public class SmsService : ISmsService
    {
        private readonly ISmsProvider smsProvider;
        private readonly ISmsRepository smsRepository;

        public SmsService(ISmsProviderFactory smsProviderFactory,
                          ISmsRepository smsRepository  )
        {
            this.smsProvider = smsProviderFactory.GetCurrentProvider();
            this.smsRepository = smsRepository;
        }
        public Task SendBulk(SmsMessageDto[] messageDtos)
        {
            return Task.CompletedTask;
        }

        public async Task SendSingle(SmsMessageDto[] messageDtos)
        {
            foreach (var message in messageDtos)
            {
                message.MobileNo = StringHelper.NormalizeMobileNumber(message.MobileNo);
                var response = await smsProvider.SendPattern(message.MobileNo,message.Message);

                smsRepository.Add(new SmsMessage()
                {
                    CorrelationId = message.CorrelationId,
                    CreatedDate = DateTime.Now,
                    Message = message.Message,
                    Recievier=message.MobileNo,
                    Result = response.Message,
                    StatusCode = response.StatusCode,
                    ProviderName = smsProvider.Key,
                    DeliveryCode = response.DeliveryCode
                });
            }
            await smsRepository.SaveAsync();
        }
    }
}
