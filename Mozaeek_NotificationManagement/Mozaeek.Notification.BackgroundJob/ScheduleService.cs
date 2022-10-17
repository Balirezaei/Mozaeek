
using Mozaeek.Notification.BackgroundJob.Configurations;
using Mozaeek.Notification.BackgroundJob.Jobs.SmsDelivery;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.BackgroundJob
{
    public class ScheduleService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger _logger;

        private readonly IScheduler scheduler;

        public ScheduleService(IServiceProvider serviceProvider, ILogger logger)
        {
            this.serviceProvider = serviceProvider;
            _logger = logger;
            StdSchedulerFactory factory = new StdSchedulerFactory();

            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();

            scheduler.JobFactory = new SmsDeliveryJobFactory(serviceProvider, _logger);

        }

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            ScheduleJobs();

        }

        public void ScheduleJobs()
        {
            scheduler.ScheduleJob(
                JobBuilder.Create<SmsDeliveryUpdateStatusJob>().Build(), DefaultTrigger());         
        }
        public ITrigger DefaultTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")                
                .WithSimpleSchedule(m =>
                {
                    m.WithInterval(TimeSpan.FromMilliseconds(5000));
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
