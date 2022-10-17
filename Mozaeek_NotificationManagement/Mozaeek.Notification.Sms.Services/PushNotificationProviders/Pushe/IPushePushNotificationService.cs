using Mozaeek.Notification.Domain.Types;
using Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe
{
    public interface IPushePushNotificationService
    {
        Task<FilteredNotificationRS> SendToSpecificClients(string appId, List<string> deviceIds, PushNotificationType notificationType, string title = null, string content = null, string customJson = null);
        Task<List<NotificationDetailsRS>> GetNotificationDetails(string wrapperId);
  
    }
}
    