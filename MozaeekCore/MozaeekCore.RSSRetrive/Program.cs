using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.RSSRetrive.Config;
using MozaeekCore.RSSRetrive.Job;

namespace MozaeekCore.RSSRetrive
{
    class Program
    {
        private static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            var services = new IocRegistration(configuration).ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            var orchestrator = (OrchestratorConfiguration)serviceProvider.GetService(typeof(OrchestratorConfiguration));
            orchestrator.Configure();
            Console.WriteLine("Hello World!");
    
    
    
        }
    }
  
}
