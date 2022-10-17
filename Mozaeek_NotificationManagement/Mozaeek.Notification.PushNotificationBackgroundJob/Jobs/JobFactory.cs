using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.PushNotificationDelivery;
using Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.SendPushNotification;
using Mozaeek.Notification.Sms.Services.Services;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.PushNotificationBackgroundJob.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var pushNotificationProviderFactory = serviceProvider.GetRequiredService<IPushNotificationService>();
            if (bundle.JobDetail.Key.Name == "Job1")
                return new PushNotificationUpdateStatusJob(pushNotificationProviderFactory);
            else
                return new SendPushNotificationJob(pushNotificationProviderFactory);

        }

        public void ReturnJob(IJob job)
        {

        }
    }
}
