using Mozaeek.Notification.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozaeek.Notification.API.ViewModels.PushNotification
{
    public class SendToSpecificClientResult
    {
        public DeliveryStatusEnum Status { get; set; }
    }
}
