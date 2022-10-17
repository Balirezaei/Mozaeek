using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mozaeek.Notification.Domain.Statics;

namespace Mozaeek.Notification.Sms.Services.SmsProviders
{
    public class SmsProviderFactory : ISmsProviderFactory
    {        
        public SmsProviderFactory(IEnumerable<ISmsProvider> smsProviders)
        {
            
            SmsProviders = smsProviders;
        }

        private IEnumerable<ISmsProvider> SmsProviders { get; }

        public ISmsProvider GetByKey(string key)
        {
            return SmsProviders.FirstOrDefault(x => x.Key == key);
        }

        public ISmsProvider GetCurrentProvider()
        {
            return SmsProviders.FirstOrDefault(x => x.Key == CurrentSettings.CurrentProvider.Name); ;
        }
    }
}
