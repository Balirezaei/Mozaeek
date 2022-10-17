using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.Sms.Services.Services;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.SendPushNotification
{
 
    [DisallowConcurrentExecution]
    public class SendPushNotificationJob: IJob
    {
        private readonly IPushNotificationService _pushNotificationService;
        public SendPushNotificationJob(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _pushNotificationService.SendToClientsFromJob();
        }
    }
}
