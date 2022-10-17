using System;
using MozaeekCore.OutboxPublisherService.Service;
using Quartz;
using Quartz.Impl;
using RssRetriveProcess.Facade;

namespace MozaeekCore.OutboxPublisher.Job
{
    public class ScheduleService
    {
        private readonly IOutboxMessageRetriveFacade _facade;
        private readonly ILogger _logger;

        private readonly IScheduler scheduler;

        public ScheduleService(IOutboxMessageRetriveFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
            StdSchedulerFactory factory = new StdSchedulerFactory();

            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();

            scheduler.JobFactory = new IntegrationJobFactory(_facade, _logger);

        }

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            ScheduleJobs();
          
        }

        public void ScheduleJobs()
        {
            scheduler.ScheduleJob(
                JobBuilder.Create<PublishToMQJob>().Build(), DefaultTrigger());


//            scheduler.ScheduleJob(DefaultTrigger()).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public ITrigger DefaultTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                // .WithCronSchedule("0/1 * * * * ?")
                .WithSimpleSchedule(m =>
                {
                    m.WithInterval(TimeSpan.FromMilliseconds(100));
                    m.RepeatForever();
                })
                //                 .WithSimpleSchedule(m=>m.WithIntervalInMinutes(7))
                //.WithSimpleSchedule(x => x
                //        .WithIntervalInSeconds(5)

                ////  .WithIntervalInSeconds(20)
                ////.StartingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(23, 50, 0)
                ////    //.WithIntervalInMinutes(2)
                ////    //.StartingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(10, 9, 0)
                ////)
                //)
                .StartNow()
                .Build();
        }
        public void Stop()
        {
            scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
