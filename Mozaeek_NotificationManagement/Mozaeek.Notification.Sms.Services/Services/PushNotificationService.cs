using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Mozaeek.Notification.Domain.Types;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Mozaeek.Notification.Sms.Services.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IPushNotificationRepository _pushNotificationRepository;
        private readonly IPushePushNotificationService _pushePushNotificationService;
        public PushNotificationService(IApplicationRepository applicationRepository, IPushNotificationRepository pushNotificationRepository, IPushePushNotificationService pushePushNotificationService)
        {
            _applicationRepository = applicationRepository;
            _pushNotificationRepository = pushNotificationRepository;
            _pushePushNotificationService = pushePushNotificationService;
        }

        public async Task SendToClientsFromJob()
        {
            var pendingNotifications =await _pushNotificationRepository.GetPushNotificationsByStatus(Domain.Types.DeliveryStatusEnum.Pending);
            pendingNotifications.ForEach( p => 
            {
                var applicationUniqueId =  _applicationRepository.GetAppId(p.ApplicationId).ConfigureAwait(false).GetAwaiter().GetResult();

                var sendNotificationResult =  _pushePushNotificationService.SendToSpecificClients(applicationUniqueId, new List<string>() { p.DeviceId }, p.NotificationType, p.Title, p.Content, p.JsonParams).ConfigureAwait(false).GetAwaiter().GetResult();
                if (sendNotificationResult == null)
                {
                    _pushNotificationRepository.UpdateDeliveryStatus(p.Id, DeliveryStatusEnum.Failed);
                }
                else
                {
                    _pushNotificationRepository.UpdateNotificationWrapperId(p.Id, sendNotificationResult.wrapper_id).ConfigureAwait(false).GetAwaiter().GetResult();
                    var notificationDetails = (_pushePushNotificationService.GetNotificationDetails(sendNotificationResult.wrapper_id).ConfigureAwait(false).GetAwaiter().GetResult()).FirstOrDefault();
                    var deliveryStatus = DeliveryStatusEnum.Sent;
                    if ((notificationDetails.statistics.delivered == notificationDetails.statistics.recipient_count) && notificationDetails.statistics.recipient_count > 0)
                        deliveryStatus = DeliveryStatusEnum.Delivered;

                    _pushNotificationRepository.UpdateDeliveryStatus(p.Id, deliveryStatus);

                }
            })
;        }
        public async Task UpdateStatusFromJob()
        {
                var pendingNotifications = (await _pushNotificationRepository.GetPushNotificationsByStatus(Domain.Types.DeliveryStatusEnum.Sent)).Where(p=>!string.IsNullOrEmpty(p.NotificationWrapperId)).ToList();
                pendingNotifications.ForEach(p =>
               {
                   Thread.Sleep(2000);
                   var notificationDetails = (_pushePushNotificationService.GetNotificationDetails(p.NotificationWrapperId).Result).FirstOrDefault();
                   var deliveryStatus = DeliveryStatusEnum.Sent;
                   if ((notificationDetails.statistics.delivered == notificationDetails.statistics.recipient_count) && notificationDetails.statistics.recipient_count > 0)
                       deliveryStatus = DeliveryStatusEnum.Delivered;
                   _pushNotificationRepository.UpdateDeliveryStatus(p.Id, deliveryStatus);
               });
        }


        public async Task<DeliveryStatusEnum> SendToSpecificClients(int applicationId, List<string> deviceIds, PushNotificationType notificationType, string title = null, string content = null, string customJson = null)
        {
            var applicationUniqueId = await _applicationRepository.GetAppId(applicationId);

            var pushNotificiationEntity = new Domain.Entity.PushNotification(title, content, (customJson != null) ?customJson:"", DeliveryStatusEnum.Pending, string.Join('_', deviceIds), notificationType, applicationId); 
            await _pushNotificationRepository.InsertAsync(pushNotificiationEntity);
            var sendNotificationResult = await _pushePushNotificationService.SendToSpecificClients(applicationUniqueId, deviceIds,notificationType, title, content, customJson);
            if (sendNotificationResult == null)
            {
                 _pushNotificationRepository.UpdateDeliveryStatus(pushNotificiationEntity.Id, DeliveryStatusEnum.Failed);
                return DeliveryStatusEnum.Failed;
            }
            await _pushNotificationRepository.UpdateNotificationWrapperId(pushNotificiationEntity.Id, sendNotificationResult.wrapper_id);
            var notificationDetails = (await _pushePushNotificationService.GetNotificationDetails(sendNotificationResult.wrapper_id)).FirstOrDefault();
            var deliveryStatus = DeliveryStatusEnum.Sent;
            if ((notificationDetails.statistics.delivered == notificationDetails.statistics.recipient_count) && notificationDetails.statistics.recipient_count > 0)
                deliveryStatus = DeliveryStatusEnum.Delivered;
           
             _pushNotificationRepository.UpdateDeliveryStatus(pushNotificiationEntity.Id, deliveryStatus);
            return deliveryStatus;

        }

    }
}
