using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Persistence;
using Mozaeek.Notification.Persistence.Repository;
using Mozaeek.Notification.Sms.Services.SmsProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.BackgroundJob.Configurations
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
                options.UseSqlServer(_configuration.GetConnectionString("NotificationContext")));
            services.AddScoped<ISmsProvider, FarazSmsProvider>();
            services.AddScoped<ISmsRepository, SmsRepository>();
            services.AddScoped<ISmsProviderFactory, SmsProviderFactory>();
            services.AddScoped<OrchestratorConfiguration>();
            services.AddScoped<ILogger, SeiLogManager>();
            return services;
        }
    }
}
