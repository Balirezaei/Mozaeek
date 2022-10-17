using MozaeekCore.OutboxPublisherService.Service;
using RssRetriveProcess.Facade;
using Topshelf;

namespace MozaeekCore.OutboxPublisher.Job
{
    public class OrchestratorConfiguration
    {
        private readonly IOutboxMessageRetriveFacade _facade;
        private readonly ILogger _logger;

        public OrchestratorConfiguration(IOutboxMessageRetriveFacade facade, ILogger logger)
        {
            _facade = facade;
            _logger = logger;
        }

        internal void Configure()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<ScheduleService>(s =>
                {
                    s.ConstructUsing(name => new ScheduleService(_facade, _logger));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                //   x.RunAsLocalSystem();
                x.SetDescription("PublishToMQJob Create Daily Letter");
                x.SetDisplayName("PublishToMQJob");
                x.SetServiceName("PublishToMQJob");
                x.StartAutomatically();
            });
        }
    }
}