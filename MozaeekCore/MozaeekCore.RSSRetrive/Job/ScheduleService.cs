using System;
using MozaeekCore.RSSRetrive.Service;
using Quartz;
using Quartz.Impl;

namespace MozaeekCore.RSSRetrive.Job
{
    public class ScheduleService
    {
        private readonly IRssRetriveFacade _facade;
        private readonly ILogger _logger;

        private readonly IScheduler scheduler;

        public ScheduleService(IRssRetriveFacade facade, ILogger logger)
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
                JobBuilder.Create<RSSMQJob>().Build(), DefaultTrigger());


        }
        public ITrigger DefaultTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(m =>
                {
                    m.WithInterval(TimeSpan.FromMinutes(2));
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
