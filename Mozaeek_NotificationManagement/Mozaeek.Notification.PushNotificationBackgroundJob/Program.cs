using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Persistence;
using Mozaeek.Notification.Persistence.Repository;
using Mozaeek.Notification.PushNotificationBackgroundJob.Configurations;
using Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe;
using Mozaeek.Notification.Sms.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozaeek.Notification.PushNotificationBackgroundJob
{
    public class Program
    {

        private static IConfigurationRoot configuration;
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
            
            configuration = builder.Build();

            var services = new IocRegistration(configuration).ConfigureServices();
            //// Generate a provider
            var serviceProvider = services.BuildServiceProvider();
            var orchestrator = (OrchestratorConfiguration)serviceProvider.GetService(typeof(OrchestratorConfiguration));
            orchestrator.Configure();
        }
    }
}

