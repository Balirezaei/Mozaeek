using Mozaeek.Notification.Domain.Entity;
using Mozaeek.Notification.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Domain.IRepository
{
    public interface IPushNotificationRepository
    {
        Task InsertAsync(PushNotification pushNotification);
        void UpdateDeliveryStatus(long id, DeliveryStatusEnum status);
        Task UpdateNotificationWrapperId(long id, string wrapperId);
        Task<List<PushNotification>> GetPushNotificationsByStatus(DeliveryStatusEnum deliveryStatus);
    }
}
