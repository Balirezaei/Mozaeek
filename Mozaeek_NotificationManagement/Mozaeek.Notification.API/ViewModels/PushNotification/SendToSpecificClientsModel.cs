using Mozaeek.Notification.Domain.Types;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mozaeek.Notification.API.ViewModels.PushNotification
{
    public class SendToSpecificClientsModel
    {
        public string Title { get; set; }

        public string Content { get; set; }
        public object CustomJson { get; set; }
        public List<string> DeviceIds { get; set; }
        public int ApplicationId { get; set; }
        public PushNotificationType NotificationType { get; set; }
        
    }
}
