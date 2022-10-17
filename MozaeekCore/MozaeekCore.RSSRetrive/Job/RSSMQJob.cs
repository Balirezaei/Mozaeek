using System;
using System.Threading.Tasks;
using MozaeekCore.RSSRetrive.Service;
using Quartz;
using Quartz.Spi;

namespace MozaeekCore.RSSRetrive.Job
{

    internal sealed class IntegrationJobFactory : IJobFactory
    {
        private readonly IRssRetriveFacade _facade;
        private readonly ILogger _logger;

        public IntegrationJobFactory(IRssRetriveFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return new RSSMQJob(_facade, _logger);

        }

        public void ReturnJob(IJob job)
        {
        }
    }

    [PersistJobDataAfterExecution]
    public class RSSMQJob : IJob
    {
        private readonly IRssRetriveFacade _facade;
        private readonly ILogger _logger;

        public RSSMQJob(IRssRetriveFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("RSSMQJob Is RAN ,{0}" + DateTime.Now.ToString("G"));
                _facade.ReadAndFetchData();
            }
            catch (Exception exception)
            {
                _logger.DoLog(exception.Message);
                if (exception.InnerException != null) _logger.DoLog(exception.InnerException.Message);
                // throw exception;
            }
        }

    }
}