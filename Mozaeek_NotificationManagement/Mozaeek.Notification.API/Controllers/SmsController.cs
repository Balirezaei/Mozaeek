using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozaeek.Notification.Domain.Dtos;
using Mozaeek.Notification.Sms.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozaeek.Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService smsService;

        public SmsController(ISmsService smsService)
        {
            this.smsService = smsService;
        }
        [HttpPost("single")]
        public Task SendSingle(SmsMessageDto messageDto)
        {
            return smsService.SendSingle(new SmsMessageDto[] { messageDto });
        }
        [HttpPost("seprate")]
        public Task SendSeprate(SmsMessageDto[] messageDtos)
        {
           return smsService.SendSingle(messageDtos);
        }
        [HttpPost("bulk")]
        public Task SendBulk(SmsMessageDto[] messageDtos)
        {
            return smsService.SendBulk(messageDtos);
        }
    }
}
