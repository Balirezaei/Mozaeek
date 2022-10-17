using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GreenPipes;
using GreenPipes.Configurators;
using GreenPipes.Policies;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.OutBoxManagement;

namespace MozaeekCore.NotificationManagementConsistency
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cnn = Configuration.GetConnectionString("MozaeekNotificationConnection");

            services.AddScoped<PushNotificationService>(sp=>
            {
                return new PushNotificationService(cnn);
            });
            services.AddScoped<IOutboxRepository, OutboxRepository>();

            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("EventStoreContext")));

            services.AddControllers();
            var massTransitSettingSection = Configuration.GetSection("MassTransitConfig");
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
             }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
