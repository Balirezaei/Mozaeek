using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Persistence;
using Mozaeek.Notification.Persistence.Repository;
using Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe;
using Mozaeek.Notification.Sms.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.PushNotificationBackgroundJob.Configurations
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

            services.AddDbContext<NotificationDbContext>(options =>
            
              options.UseSqlServer(_configuration.GetConnectionString("NotificationContext")),ServiceLifetime.Transient);

            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IPushNotificationRepository, PushNotificationRepository>();
            services.AddTransient<IPushNotificationService, PushNotificationService>();
            services.AddTransient<OrchestratorConfiguration>();
            services.AddTransient<IPushePushNotificationService, PushePushNotificationService>();

            return services;

        }
    }
}
