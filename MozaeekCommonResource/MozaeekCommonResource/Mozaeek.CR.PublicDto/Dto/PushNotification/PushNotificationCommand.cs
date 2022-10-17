using System;
using Mozaeek.CR.PublicDto.Enum;

namespace Mozaeek.CR.PublicDto.Dto
{
    public class PushNotificationMessage
    {
        public string ReceiverId { get; set; }
        public PushNotificationType Type { get; set; }
        public string Payload { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}