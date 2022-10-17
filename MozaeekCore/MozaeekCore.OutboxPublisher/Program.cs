using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Domain.Contract.BasicInfo;
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


            //configuration = builder.Build();

            var services = new IocRegistration(configuration).ConfigureServices();
            //// Generate a provider
            var serviceProvider = services.BuildServiceProvider();

   
            //Console.WriteLine("Hello World!");

            //var publishEndpoint = (IPublishEndpoint)serviceProvider.GetService(typeof(IPublishEndpoint));
            //Thread.Sleep(5000);
            //publishEndpoint.Publish<LabelCreatedOrUpdated>(new LabelCreatedOrUpdated(11, "test bbbbbahar", null, true));

            //while (true)
            //{
            //    //publishEndpoint.Publish<Register>(new Register() { CreateDate = DateTime.Now, Title = "test" });
            //}

            var orchestrator = (OrchestratorConfiguration)serviceProvider.GetService(typeof(OrchestratorConfiguration));
            orchestrator.Configure();

        }
    }

}
