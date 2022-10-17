using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Messaging.RabbitMQ;
using GreenPipes;
using GreenPipes.Configurators;
using GreenPipes.Policies;
using MassTransit.Topology;
using MozaeekCore.Core.MessagePublisher;
using Newtonsoft.Json;

namespace MozaeekCore.RestAPI.Bootstrap
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            //var massTransitSettingSection = configuration.GetSection("MassTransitConfig");
            //var massTransitConfig = massTransitSettingSection.Get<MassTransitConfig>();

            //services.AddMassTransit(scConfig =>
            //{
            //    scConfig.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmqConfig =>
            //    {
            //        //rmqConfig.UseExtensionsLogging(provider.GetRequiredService<ILoggerFactory>());

            //        // Force serialization of default values: null, false, etc
            //        rmqConfig.ConfigureJsonSerializer(jsonSettings =>
            //        {
            //            jsonSettings.DefaultValueHandling = DefaultValueHandling.Include;
            //            return jsonSettings;
            //        });

            //        var nameFormatter = new BusEnvironmentNameFormatter(rmqConfig.MessageTopology.EntityNameFormatter, busSettings);
            //        var host = rmqConfig.Host(new Uri(rabbitMqSettings.ConnectionString), hostConfig =>
            //        {
            //            hostConfig.Username(massTransitConfig.Username);
            //            hostConfig.Password(massTransitConfig.Password);
            //        });

            //        // Endpoint with custom naming
            //        rmqConfig.ReceiveEndpoint(host, nameFormatter.Format(busSettings.Endpoint), epConfig =>
            //        {
            //            epConfig.PrefetchCount = busSettings.MessagePrefetchCount;
            //            epConfig.UseMessageRetry(x => x.Interval(busSettings.MessageRetryCount, busSettings.MessageRetryInterval));
            //            epConfig.UseInMemoryOutbox();

            //            //TODO: Bind messages to this queue/endpoint
            //            epConfig.MapMessagesToConsumers(provider, busSettings);
            //        });

            //        // Custom naming for exchanges
            //        rmqConfig.MessageTopology.SetEntityNameFormatter(nameFormatter);
            //    }));
            //});



            //services.AddMassTransit(x =>
            //{
            //    //  x.AddConsumers(Assembly.GetExecutingAssembly());
            //    x.SetKebabCaseEndpointNameFormatter();
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.ConfigureEndpoints(context);
            //        cfg.OverrideDefaultBusEndpointQueueName("test");
            //        cfg.UseMessageRetry(r =>
            //        {
            //            r.SetRetryPolicy((RetryPolicyFactory)(filter => (IRetryPolicy)new NoRetryPolicy(filter)));
            //            //r.Immediate(0);
            //            //r.Interval(0, 0);
            //        });
            //        cfg.Host(massTransitConfig.Host, massTransitConfig.VirtualHost,
            //            h =>
            //            {
            //                h.Username(massTransitConfig.Username);
            //                h.Password(massTransitConfig.Password);
            //            }
            //        );
            //    });
            //});

            //services.AddMassTransitHostedService();

            //Order is important           
           // services.AddScoped<IMessagePublisher, MassTransitMqPublisher>();
            services.AddScoped<IMessagePublisher, MozaeekCore.OutBoxManagement.OutboxPublisher>();
        }
    }

    class EnvironmentNameFormatter : IEntityNameFormatter
    {
        private IEntityNameFormatter _original;
        private readonly string _environment;

        public EnvironmentNameFormatter(IEntityNameFormatter original, string environment)
        {
            _original = original;
            _environment = environment;
        }

        public string FormatEntityName<T>()
        {

            return $"{_environment}:{_original.FormatEntityName<T>()}";
        }
    }
}
