using Mozaeek.Notification.Domain.Types;
using Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe.Dto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.PushNotificationProviders.Pushe
{
    public class PushePushNotificationService : IPushePushNotificationService
    {
        public async Task<List<NotificationDetailsRS>> GetNotificationDetails(string wrapperId)
        {
            var _client = new RestClient($"https://api.pushe.co/v2/messaging/notifications/{wrapperId}/statistics/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("authorization", "Token 0031d2bf66c5b052031b8732ccaf0f56710f34de");
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<List<NotificationDetailsRS>>(response.Content);
        }

        public async Task<FilteredNotificationRS> SendToSpecificClients(string appId, List<string> deviceIds,PushNotificationType notificationType, string title = null, string content = null, string customJson = null)
        {
            try
            {
                var _client = new RestClient("https://api.pushe.co/v2/messaging/notifications/");
                var request = new RestRequest(Method.POST);
                if (notificationType==PushNotificationType.JsonMessage)
                {
                    
                    var requestBody = new Custom_ContentNotificationRQ() { app_ids = appId, filters = new Filters() { device_id = deviceIds.ToArray() }, custom_content = customJson };
                    request.AddJsonBody(requestBody);
                }
                else if(notificationType == PushNotificationType.JsonNotif)
                {
                    var requestBody = new CustomFilteredNotificationRQ() { app_ids = appId, data = new Data() { content = content, title = title }, filters = new Filters() { device_id = deviceIds.ToArray() } ,custom_content=customJson};
                    request.AddJsonBody(requestBody);
                }
                else
                {
                    var requestBody = new FilteredNotificationRQ() { app_ids = appId, data = new Data() { content = content, title = title }, filters = new Filters() { device_id = deviceIds.ToArray() } };
                    request.AddJsonBody(requestBody);
                }



                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("authorization", "Token 0031d2bf66c5b052031b8732ccaf0f56710f34de");
                var response = await _client.ExecuteAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Created && response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;
                return JsonConvert.DeserializeObject<FilteredNotificationRS>(response.Content);
            }
            catch(Exception ex)
            {
                return null;
            }
        }


    }
}
