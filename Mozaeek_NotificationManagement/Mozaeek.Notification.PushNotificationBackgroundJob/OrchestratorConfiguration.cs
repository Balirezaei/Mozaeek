using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace Mozaeek.Notification.PushNotificationBackgroundJob
{
    public class OrchestratorConfiguration
    {
        private readonly IServiceProvider serviceProvider;

        public OrchestratorConfiguration(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Configure()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<ScheduleService>(s =>
                {
                    s.ConstructUsing(name => new ScheduleService(serviceProvider));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                //   x.RunAsLocalSystem();
                x.SetDescription("PushNotificationDeliveryJob Create Daily Letter");
                x.SetDisplayName("PushNotification1");
                x.SetServiceName("PushNotification1");
                
                x.StartAutomatically();
             
            });
        }
    }
}
