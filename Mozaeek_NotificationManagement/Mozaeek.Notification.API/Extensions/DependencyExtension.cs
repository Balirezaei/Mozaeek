using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Persistence.Repository;
using Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe;
using Mozaeek.Notification.Sms.Services.Services;
using Mozaeek.Notification.Sms.Services.SmsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozaeek.Notification.API.Extensions
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<ISmsProvider, FarazSmsProvider>();
            services.AddScoped<ISmsRepository, SmsRepository>();
            services.AddScoped<ISmsProviderFactory, SmsProviderFactory>();
            services.AddScoped<IPushePushNotificationService, PushePushNotificationService>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IPushNotificationRepository, PushNotificationRepository>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
        }
    }
}
