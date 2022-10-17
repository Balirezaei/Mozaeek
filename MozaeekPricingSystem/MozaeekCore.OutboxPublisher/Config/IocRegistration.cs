using System;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.RabbitMqTransport.Configuration;
using MassTransit.Topology;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.OutboxPublisher.Job;
using MozaeekCore.OutboxPublisherService.Service;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.ReadConsistencyService.Consumers;
using MozaeekCore.ReadConsistencyService.Consumers.BasicInfo;
using MozaeekCore.ReadConsistencyService.Extensions;
using Newtonsoft.Json;
using RssRetriveProcess.Facade;

namespace MozaeekCore.OutboxPublisher.Config
{
    public class IocRegistration
    {
        private readonly IConfigurationRoot _configuration;

        public IocRegistration(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("EventStoreContext")));

            services.AddScoped<OrchestratorConfiguration>();

            services.AddScoped<IOutboxMessageRetriveFacade, OutboxMessageRetriveFacade>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();

            var massTransitSettingSection = _configuration.GetSection("MassTransitConfig");
            var massTransitConfig = massTransitSettingSection.Get<MassTransitConfig>();

            services.AddScoped<ILogger, SeiLogManager>();
            services.AddMassTransit(scConfig =>
            {
                scConfig.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.ConfigureJsonSerializer(jsonSettings =>
            {
                jsonSettings.DefaultValueHandling = DefaultValueHandling.Include;
                return jsonSettings;
            });
                    cfg.Host(massTransitConfig.Host, massTransitConfig.VirtualHost,
                          h =>
                          {
                              h.Username(massTransitConfig.Username);
                              h.Password(massTransitConfig.Password);
                          }
                      );
                    var sp = services.BuildServiceProvider();

                    AllEndPoint.Set(massTransitConfig, sp, cfg);


                }));
            });


            services.AddScoped<IMessagePublisher, MassTransitMqPublisher>();
            return services;
        }

      

    }

}
