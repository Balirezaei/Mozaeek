using Mozaeek.Notification.Domain.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mozaeek.Notification.Domain.Entity
{
    public class PushNotification
    {
        public PushNotification(string title,string content,string jsonParams, DeliveryStatusEnum deliveryStatus,string deviceId,PushNotificationType notificationType,int applicationId)
        {
            CreationDate = DateTime.Now;
            Title = title;
            Content = content;
            JsonParams = jsonParams;
            DeliveryStatus = deliveryStatus;
            DeviceId = deviceId;
            ApplicationId = applicationId;
            NotificationType = notificationType;

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string JsonParams { get; set; }
        public DeliveryStatusEnum DeliveryStatus { get; set; }
        public string DeviceId { get; set; }
        public int ApplicationId { get; set; }
        public string NotificationWrapperId { get; set; }
        public PushNotificationType NotificationType { get; set; }
        public virtual Application Application { get; set; }
    }
}
