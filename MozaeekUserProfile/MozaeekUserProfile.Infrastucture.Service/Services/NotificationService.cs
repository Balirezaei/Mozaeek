using Microsoft.Extensions.Options;
using MozaeekUserProfile.Domain.Contracts;
using MozaeekUserProfile.Domain.OptionSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Infrastucture.Service.Services
{
    public class NotificationService: IOtpSenderService
    {
        private readonly HttpClient client;

        public NotificationService(HttpClient client,
                                   IOptions<MicroserviceUrlsSettings>options)
        {
            this.client = client;
            this.client.BaseAddress = new Uri( options.Value.NotificationService);
            this.client.DefaultRequestHeaders.Add("accept", "application/json");
            
        }
        public async Task SendOtp(string phoneNumber,string message)
        {
            var urn = "api/sms/single";
            
            var model = JsonConvert.SerializeObject(new { MobileNo=phoneNumber, Message=message, CorrelationId="otp" });
            var response = await client.PostAsync(urn, new StringContent(model,Encoding.UTF8,"application/json"));
            response.EnsureSuccessStatusCode();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new System.Exception("Could not Stablish Http Connetion!");
            }
        }
    }
}
