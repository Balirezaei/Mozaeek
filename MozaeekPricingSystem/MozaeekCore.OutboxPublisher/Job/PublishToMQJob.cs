using System;
using System.Threading.Tasks;
using MozaeekCore.OutboxPublisherService.Service;
using Quartz;
using Quartz.Spi;
using RssRetriveProcess.Facade;

namespace MozaeekCore.OutboxPublisher.Job
{

    internal sealed class IntegrationJobFactory : IJobFactory
    {
        private readonly IOutboxMessageRetriveFacade _facade;
        private readonly ILogger _logger;

        public IntegrationJobFactory(IOutboxMessageRetriveFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return new PublishToMQJob(_facade, _logger);

        }

        public void ReturnJob(IJob job)
        {
        }
    }

    [DisallowConcurrentExecution]
    public class PublishToMQJob : IJob
    {
        private readonly IOutboxMessageRetriveFacade _facade;
        private readonly ILogger _logger;

        public PublishToMQJob(IOutboxMessageRetriveFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // Console.WriteLine("PublishToMQJob Is RAN ,{0}" + DateTime.Now.ToString("G"));
                await _facade.SendOutboxMessageToQue();
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