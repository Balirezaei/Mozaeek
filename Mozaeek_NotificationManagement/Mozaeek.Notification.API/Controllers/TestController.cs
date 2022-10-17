using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozaeek.Notification.Sms.Services.SmsProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozaeek.Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ISmsProvider smsProvider;

        public TestController(ISmsProviderFactory smsProviderFactory)
        {
            smsProvider =  smsProviderFactory.GetCurrentProvider();
        }
        [HttpPost]
        public async Task Post()
        {
            await smsProvider.GetDelivery(170858572);
        }
    }
}
