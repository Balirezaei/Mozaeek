using System;
using System.Reflection;
using GreenPipes;
using GreenPipes.Configurators;
using GreenPipes.Policies;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.OutboxPublisher.Job;
using MozaeekCore.OutboxPublisherService.Service;
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

            services.AddScoped<ServiceInformation>(provider =>
            {
                var serviceInfo = new ServiceInformation(_configuration.GetValue<String>("ServiceName"));
                return serviceInfo;
            });

            services.AddScoped<IOutboxMessageRetriveFacade, OutboxMessageRetriveFacade>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            
            var massTransitSettingSection = _configuration.GetSection("MassTransitConfig");
            var massTransitConfig = massTransitSettingSection.Get<MassTransitConfig>();
            
            services.AddScoped<ILogger, SeiLogManager>();
            services.AddMassTransit(scConfig =>
            {
                scConfig.SetKebabCaseEndpointNameFormatter();
                scConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r =>
                    {
                        r.SetRetryPolicy((RetryPolicyFactory)(filter => (IRetryPolicy)new NoRetryPolicy(filter)));
                    });
                    cfg.Host(massTransitConfig.Host, massTransitConfig.VirtualHost,
                        h =>
                        {
                            h.Username(massTransitConfig.Username);
                            h.Password(massTransitConfig.Password);
                        }
                    );
                });
            });

            services.AddMassTransitHostedService();

            services.AddScoped<IMessagePublisher, MassTransitMqPublisher>();
            return services;
        }
    }

}
