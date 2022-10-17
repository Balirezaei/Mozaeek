using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe.Dto
{
    public class CustomFilteredNotificationRQ
    {
        public string app_ids { get; set; }
        public Filters filters { get; set; }
        public Data data { get; set; }
        public string custom_content { get; set; }
    }
}
