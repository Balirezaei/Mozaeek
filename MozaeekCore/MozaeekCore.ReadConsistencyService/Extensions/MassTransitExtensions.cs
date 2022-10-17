using System;
using System.Reflection;
using System.Text;
using GreenPipes;
using GreenPipes.Configurators;
using GreenPipes.Policies;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.ReadConsistencyService.Consumers;
using MozaeekCore.ReadConsistencyService.Consumers.BasicInfo;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MozaeekCore.ReadConsistencyService.Extensions
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSettingSection = configuration.GetSection("MassTransitConfig");
            var massTransitConfig = massTransitSettingSection.Get<MassTransitConfig>();

            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
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

            Log.Logger = (Serilog.ILogger)new LoggerConfiguration()
                .WriteTo
                .File("log-.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Information().CreateLogger();
            Log.Logger.Information("test");

            services.AddMassTransitHostedService();
            //.AddLogging();
        }
    }
}
