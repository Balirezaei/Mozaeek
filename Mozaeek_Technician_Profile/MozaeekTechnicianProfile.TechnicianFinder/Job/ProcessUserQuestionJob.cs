using System;
using System.Threading.Tasks;
using MozaeekTechnicianProfile.TechnicianFinder.Service;
using Quartz;
using Quartz.Spi;

namespace MozaeekTechnicianProfile.TechnicianFinder.Job
{

    internal sealed class IntegrationJobFactory : IJobFactory
    {
        private readonly IProperTechnicianFinderServiceFacade _facade;
        private readonly ILogger _logger;

        public IntegrationJobFactory(ILogger logger, IProperTechnicianFinderServiceFacade facade)
        {
            _logger = logger;
            _facade = facade;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return new ProcessUserQuestionJob(_facade, _logger);

        }

        public void ReturnJob(IJob job)
        {
        }
    }


    [DisallowConcurrentExecution]
    public class ProcessUserQuestionJob : IJob
    {
        private readonly IProperTechnicianFinderServiceFacade _facade;
        private readonly ILogger _logger;

        public ProcessUserQuestionJob(IProperTechnicianFinderServiceFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("ProcessUserQuestionJob Is RAN ,{0}" + DateTime.Now.ToString("G"));
                await _facade.FindTechnicianForWaitingQuestion();
            }
            catch (System.Exception exception)
            {
                _logger.DoLog(exception.Message);
                if (exception.InnerException != null) _logger.DoLog(exception.InnerException.Message);
                // throw exception;
            }
        }

    }
}