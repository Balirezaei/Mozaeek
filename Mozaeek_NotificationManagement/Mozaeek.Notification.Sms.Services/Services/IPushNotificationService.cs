using Mozaeek.Notification.Domain.Types;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.Services
{
    public interface IPushNotificationService
    {
        Task<DeliveryStatusEnum> SendToSpecificClients(int applicationId, List<string> deviceIds,PushNotificationType notificationType, string title = null, string content = null, string customJson = null);
        Task SendToClientsFromJob();
        Task UpdateStatusFromJob();
    }
}
