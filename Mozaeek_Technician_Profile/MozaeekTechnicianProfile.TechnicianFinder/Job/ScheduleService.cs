using System;
using MozaeekTechnicianProfile.TechnicianFinder.Service;
using Quartz;
using Quartz.Impl;

namespace MozaeekTechnicianProfile.TechnicianFinder.Job
{
    public class ScheduleService
    {
        private readonly IProperTechnicianFinderServiceFacade _facade;
        private readonly ILogger _logger;

        private readonly IScheduler scheduler;

        public ScheduleService(IProperTechnicianFinderServiceFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
            StdSchedulerFactory factory = new StdSchedulerFactory();

            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();

            scheduler.JobFactory = new IntegrationJobFactory(_logger, _facade);

        }

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            ScheduleJobs();

        }

        public void ScheduleJobs()
        {
            scheduler.ScheduleJob(
                JobBuilder.Create<ProcessUserQuestionJob>().Build(), DefaultTrigger());


        }
        public ITrigger DefaultTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(m =>
                {
                    m.WithInterval(TimeSpan.FromSeconds(30));
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
