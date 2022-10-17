using Mozaeek.Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mozaeek.Notification.Domain.Statics
{
    public class CurrentSettings
    {
        private static SmsProvider[] SmsProviders { get; set; }
        public static SmsProvider CurrentProvider { 
            get {
                return SmsProviders.FirstOrDefault(x => x.IsActive);
            }
        }
        public static void SetProviders(SmsProvider[] smsProviders)
        {
            SmsProviders = smsProviders;
        }
        public static SmsProvider GetProvider(string name)
        {
            return SmsProviders.FirstOrDefault(x => x.Name == name);
        }
    }
}
