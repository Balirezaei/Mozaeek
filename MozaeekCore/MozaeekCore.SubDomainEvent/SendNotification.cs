using System;

namespace MozaeekCore.SubDomainEvent
{
    public class SendNotification
    {
        /// <summary>
        /// اس ام اس = 1
        /// 2=Email
        /// 3=...
        /// </summary>
        public int NotificationType { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string[] Receiver { get; set; }
    }
}
