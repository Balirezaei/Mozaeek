using System.Reflection;
using GreenPipes;
using GreenPipes.Configurators;
using GreenPipes.Policies;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozaeek.CR.PublicEvent.UserProfile;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.TechnicianProfileConsistensyService.Consumer;

namespace MozaeekCore.TechnicianProfileConsistensyService.Extension
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
            services.AddMassTransitHostedService();
        }
    }
}