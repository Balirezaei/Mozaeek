using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe.Dto
{


    public class FilteredNotificationRQ
    {
        public string app_ids { get; set; }
        public Filters filters { get; set; }
        public Data data { get; set; }
    }

    public class Filters
    {
        public string[] device_id { get; set; }
        public string[] brand { get; set; }
        public string[] app_version { get; set; }
    }

    public class Data
    {
        public string title { get; set; }
        public string content { get; set; }
    }


}
