using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe.Dto
{


    public class FilteredNotificationRS
    {
        public string wrapper_id { get; set; }
        public string hashed_id { get; set; }
        public string[] app_ids { get; set; }
        public Data data { get; set; }
        public int data_type { get; set; }
        public Custom_Content custom_content { get; set; }
        public object abt_ids { get; set; }
        public object topics { get; set; }
        public Filters filters { get; set; }
        public object segment_ids { get; set; }
        public object[] segment_names { get; set; }
        public object rate_limit { get; set; }
        public object max_recipients { get; set; }
        public bool unique { get; set; }
        public Statistics statistics { get; set; }
        public int type { get; set; }
        public int content_type { get; set; }
        public object priority { get; set; }
        public int platform { get; set; }
        public int message_type { get; set; }
        public DateTime created_at { get; set; }
        public object dispatch_started_at { get; set; }
        public object dispatch_ended_at { get; set; }
        public object updated_at { get; set; }
        public int time_to_live { get; set; }
        public object collapse_key { get; set; }
        public object eta { get; set; }
        public object update_av_code { get; set; }
        public int status { get; set; }
        public object csv_id { get; set; }
        public object sender_config { get; set; }
    }

 

    public class Custom_Content
    {
    }



    public class Statistics
    {
        public int recipient_count { get; set; }
        public int accurate_recipients { get; set; }
        public int delivered { get; set; }
        public int clicked { get; set; }
        public int dismissed { get; set; }
        public int nacked { get; set; }
        public int acked { get; set; }
        public int published { get; set; }
    }


}
