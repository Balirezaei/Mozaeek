using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.OutboxPublisher.Config;
using MozaeekCore.OutboxPublisher.Job;
using Newtonsoft.Json;

namespace MozaeekCore.OutboxPublisher
{
    class Program
    {
        private static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
    
            configuration = builder.Build();
    
            var myConnString = configuration.GetConnectionString("EventStoreContext");
            
    
            var massTransitSettingSection = configuration.GetSection("MassTransitConfig");
            var massTransitConfig = massTransitSettingSection.Get<MassTransitConfig>();
            
            Console.WriteLine(massTransitConfig.EndPoint);
    
            //configuration = builder.Build();
    
            var services = new IocRegistration(configuration).ConfigureServices();
            //// Generate a provider
            var serviceProvider = services.BuildServiceProvider();
    
            var orchestrator = (OrchestratorConfiguration)serviceProvider.GetService(typeof(OrchestratorConfiguration));
            orchestrator.Configure();
            Console.WriteLine("Hello World!");
    
    
    
        }
    }
  
}
