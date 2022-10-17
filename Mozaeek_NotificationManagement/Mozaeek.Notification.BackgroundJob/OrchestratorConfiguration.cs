
using Mozaeek.Notification.BackgroundJob.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace Mozaeek.Notification.BackgroundJob
{
    public class OrchestratorConfiguration
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger _logger;

        public OrchestratorConfiguration(IServiceProvider serviceProvider, ILogger logger)
        {
            this.serviceProvider = serviceProvider;
            _logger = logger;
        }

        internal void Configure()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<ScheduleService>(s =>
                {
                    s.ConstructUsing(name => new ScheduleService(serviceProvider, _logger));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                //   x.RunAsLocalSystem();
                x.SetDescription("SmsDeliveryJob Create Daily Letter");
                x.SetDisplayName("SmsDeliveryJob");
                x.SetServiceName("SmsDeliveryJob");
                x.StartAutomatically();
            });
        }
    }
}
