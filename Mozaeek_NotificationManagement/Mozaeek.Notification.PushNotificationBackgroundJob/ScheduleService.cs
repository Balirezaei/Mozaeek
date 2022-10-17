using Mozaeek.Notification.PushNotificationBackgroundJob.Jobs;
using Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.PushNotificationDelivery;
using Mozaeek.Notification.PushNotificationBackgroundJob.Jobs.SendPushNotification;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.PushNotificationBackgroundJob
{

    public class ScheduleService
    {
        private readonly IServiceProvider serviceProvider;

        private readonly IScheduler scheduler; 
        public ScheduleService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            StdSchedulerFactory factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
            scheduler.JobFactory = new JobFactory(serviceProvider);
        }

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            ScheduleJobs();

        }

        public void ScheduleJobs()
        {
            scheduler.ScheduleJob(
                JobBuilder.Create<PushNotificationUpdateStatusJob>().WithIdentity("Job1").Build(), DefaultTrigger());
            scheduler.ScheduleJob(
             JobBuilder.Create<SendPushNotificationJob>().WithIdentity("Job2").Build(), DefaultTrigger2());
        }
        public ITrigger DefaultTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(m =>
                {
                    m.WithInterval(TimeSpan.FromMilliseconds(1000));
                    m.RepeatForever();
                })
                .StartNow()
                .Build();
        }
        public ITrigger DefaultTrigger2()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger2", "group1")
                .WithSimpleSchedule(m =>
                {
                    m.WithInterval(TimeSpan.FromMilliseconds(1000));
                    m.RepeatForever();
                })
                .StartNow()
                .Build();
        }
        public void Stop()
        {
            scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
