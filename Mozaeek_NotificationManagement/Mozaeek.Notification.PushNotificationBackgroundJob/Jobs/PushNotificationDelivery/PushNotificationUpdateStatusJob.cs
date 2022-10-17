using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.SendPushNotification;
using Mozaeek.Notification.Sms.Services.Services;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.PushNotificationDelivery
{
 
    [DisallowConcurrentExecution]
    public class PushNotificationUpdateStatusJob : IJob
    {
        private readonly IPushNotificationService _pushNotificationService;
        public PushNotificationUpdateStatusJob(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
             await _pushNotificationService.UpdateStatusFromJob();
        }
    }
}
