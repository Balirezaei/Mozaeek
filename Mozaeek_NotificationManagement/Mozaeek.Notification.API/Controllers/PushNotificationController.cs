using Microsoft.AspNetCore.Mvc;
using Mozaeek.Notification.API.ViewModels.PushNotification;
using Mozaeek.Notification.Sms.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozaeek.Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationController : Controller
    {
        private readonly IPushNotificationService _pushNotificationService;
        public PushNotificationController(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        public async Task<SendToSpecificClientResult> SendToSpecificClient(SendToSpecificClientsModel input)
        {
            var result=await _pushNotificationService.SendToSpecificClients(input.ApplicationId, input.DeviceIds,input.NotificationType, input.Title, input.Content,(input.CustomJson!=null)?input.CustomJson.ToString():null);
            return new SendToSpecificClientResult() { Status = result };
        }
    }
}
