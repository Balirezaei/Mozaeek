using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Messaging.RabbitMQ;

namespace MozaeekCore.ReadConsistencyService.Extensions
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var massTransitSettingSection = configuration.GetSection("MassTransitConfig");
            var massTransitConfig = massTransitSettingSection.Get<MassTransitConfig>();
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(massTransitConfig.Host, massTransitConfig.VirtualHost, h =>
                {
                    h.Username(massTransitConfig.Username);
                    h.Password(massTransitConfig.Password);
                });
                var sp = services.BuildServiceProvider();
                AllEndPoint.Set(massTransitConfig, sp, cfg);
            });
            busControl.Start();
        }
    }
}
