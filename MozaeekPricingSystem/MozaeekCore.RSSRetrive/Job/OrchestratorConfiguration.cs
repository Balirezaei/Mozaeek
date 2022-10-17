using MozaeekCore.RSSRetrive.Service;
using Topshelf;

namespace MozaeekCore.RSSRetrive.Job
{
    public class OrchestratorConfiguration
    {
        private readonly IRssRetriveFacade _facade;
        private readonly ILogger _logger;

        public OrchestratorConfiguration(IRssRetriveFacade facade, ILogger logger)
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
                x.SetDescription("RSSRetrive");
                x.SetDisplayName("RSSRetrive");
                x.SetServiceName("RSSRetrive");
                x.StartAutomatically();
            });
        }
    }
}