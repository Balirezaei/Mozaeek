using Microsoft.Extensions.DependencyInjection;
using Mozaeek.Notification.BackgroundJob.Configurations;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Domain.Statics;
using Mozaeek.Notification.Sms.Services.SmsProviders;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.BackgroundJob.Jobs.SmsDelivery
{
    public class SmsDeliveryJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;

        public SmsDeliveryJobFactory(IServiceProvider serviceProvider,ILogger logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var smsProviderFactory = serviceProvider.GetRequiredService<ISmsProviderFactory>();
            var smsRepository = serviceProvider.GetRequiredService<ISmsRepository>();
            return new SmsDeliveryUpdateStatusJob(smsProviderFactory, smsRepository);
        }

        public void ReturnJob(IJob job)
        {
          
        }
    }
    [DisallowConcurrentExecution]
    public class SmsDeliveryUpdateStatusJob : IJob
    {
        private readonly ISmsProviderFactory smsProviderFactory;
        private readonly ISmsRepository smsRepository;

        public SmsDeliveryUpdateStatusJob(ISmsProviderFactory smsProviderFactory, 
                                          ISmsRepository smsRepository)
        {
            this.smsProviderFactory = smsProviderFactory;
            this.smsRepository = smsRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var nonFinalStatusSmsList = await smsRepository.GetForDeliveryStatusUpdate();            
            foreach (var sms in nonFinalStatusSmsList)
            {
                if (string.IsNullOrWhiteSpace(sms.ProviderName))
                {
                    continue;
                }                
                var provider = smsProviderFactory.GetByKey(sms.ProviderName);
                var deliveryStatus = await provider.GetDelivery(sms.DeliveryCode);

                if (deliveryStatus == null)
                {
                    continue;
                }                
                if (deliveryStatus!=null && deliveryStatus.Length > 0)
                {
                    var currentStatusNumber = SmsDeliveryStates.GetStatusNumber(deliveryStatus[0].Status);
                    

                    if (currentStatusNumber != sms.DeliveryStatus)
                    {
                        sms.DeliveryStatus = currentStatusNumber;
                    }
                }
            }
            await smsRepository.SaveAsync();
        }
    }
}
