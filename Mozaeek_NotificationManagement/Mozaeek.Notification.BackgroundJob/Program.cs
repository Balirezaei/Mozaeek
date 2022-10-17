using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.BackgroundJob.Configurations;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Domain.Statics;
using System;
using System.Threading.Tasks;

namespace Mozaeek.Notification.BackgroundJob
{
    class Program
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
            using (var scope = serviceProvider.CreateScope())
            {
                var smsRepository = scope.ServiceProvider.GetRequiredService<ISmsRepository>();
                CurrentSettings.SetProviders(await smsRepository.GetProviders());
            }

            var orchestrator = (OrchestratorConfiguration)serviceProvider.GetService(typeof(OrchestratorConfiguration));
            orchestrator.Configure();
        }
    }
}
