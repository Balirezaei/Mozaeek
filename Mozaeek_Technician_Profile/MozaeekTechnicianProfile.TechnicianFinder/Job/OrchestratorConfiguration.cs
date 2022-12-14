using MozaeekTechnicianProfile.TechnicianFinder.Config;
using MozaeekTechnicianProfile.TechnicianFinder.Service;
using Topshelf;

namespace MozaeekTechnicianProfile.TechnicianFinder.Job
{
    public class OrchestratorConfiguration
    {
        private readonly IProperTechnicianFinderServiceFacade _facade;
        private readonly ILogger _logger; 
        private readonly ServiceInformation _serviceInformation;

        public OrchestratorConfiguration(IProperTechnicianFinderServiceFacade facade, ILogger logger, ServiceInformation serviceInformation)
        {
            _facade = facade;
            _logger = logger;
            _serviceInformation = serviceInformation;
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
                x.SetDescription($"{_serviceInformation.ServiceName} Create Daily Letter");
                x.SetDisplayName($"{_serviceInformation.ServiceName}");
                x.SetServiceName($"{_serviceInformation.ServiceName}");
                x.StartAutomatically();
            });
        }
    }
}