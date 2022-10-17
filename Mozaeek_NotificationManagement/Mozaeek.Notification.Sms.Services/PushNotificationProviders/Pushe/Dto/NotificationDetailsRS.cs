using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe.Dto
{



    public class NotificationDetailsRS
    {
        public string app_id { get; set; }
        public int notification_id { get; set; }
        public Statisticss statistics { get; set; }
        public object owner_id { get; set; }
        public bool is_active { get; set; }
        public App_Info app_info { get; set; }
    }

    public class Statisticss
    {
        public int delivered { get; set; }
        public int clicked { get; set; }
        public int dismissed { get; set; }
        public int nacked { get; set; }
        public int acked { get; set; }
        public int published { get; set; }
        public int recipient_count { get; set; }
        public int accurate_recipients { get; set; }
    }

    public class App_Info
    {
        public string name { get; set; }
        public object icon_url { get; set; }
    }


}
