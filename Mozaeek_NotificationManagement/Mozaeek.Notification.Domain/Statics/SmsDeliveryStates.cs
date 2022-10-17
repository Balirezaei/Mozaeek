using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Domain.Statics
{
    public class SmsDeliveryStates
    {
        public const string Sending = "notsync";
        public const string Send = "send";
        public const string Pending = "pending";
        public const string Failed = "failed";
        public const string Discarded = "discarded";
        public const string Delivered= "delivered";
        public static int GetStatusNumber(string status)
        => status switch
        {
            Sending=>0,
            Send=>1,
            Pending=>2,
            Failed=>3,
            Discarded=>4,
            Delivered=>5,
            _=>-1
        };
        public static int[] GetNonFinalStatuses()
        {
            return new int[] { GetStatusNumber(Sending), GetStatusNumber(Pending) };
        }
    }
}
