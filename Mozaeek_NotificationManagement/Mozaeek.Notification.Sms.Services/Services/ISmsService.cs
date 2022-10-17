using Mozaeek.Notification.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.Services
{
    public interface ISmsService
    {
        Task SendSingle(SmsMessageDto[] messageDtos);
        Task SendBulk(SmsMessageDto[] messageDtos);
    }
}
