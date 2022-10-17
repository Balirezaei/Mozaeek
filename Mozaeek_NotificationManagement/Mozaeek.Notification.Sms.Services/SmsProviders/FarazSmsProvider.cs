using Mozaeek.Notification.Domain.Dtos;
using Mozaeek.Notification.Domain.Statics;
using Mozaeek.Notification.Domain.Types;
using Mozaeek.Notification.Sms.Services.Utility;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.SmsProviders
{
    public class FarazSmsProvider : ISmsProvider
    {
        private  string URL ;
        private  string userName ;
        private  string password ;
        private  string senderNumber ;
        private const string patternCode = "0jbescok0y";
        public FarazSmsProvider()
        {
            var providerSettings = CurrentSettings.GetProvider(Key);
            URL = providerSettings.Url;
            userName = providerSettings.UserName;
            password = providerSettings.Password;
            senderNumber = providerSettings.SenderNumber;
        }

        public string Key { get => "faraz"; }

        public async Task<SmsResponseDto>  Send(string recipient, string message)
        {
            
            string recipients = $"[\"{recipient}\"]";
            var client = new RestClient(URL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\"op\" : \"send\"" +
                $",\"uname\" : \"{userName}\"" +
                $",\"pass\":  \"{password}\"" +
                $",\"message\" : \"{message}\"" +
                $",\"from\": \"{senderNumber}\"" +
                $",\"to\" : {recipients}"+"}"
                , ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            var contentResult = JsonConvert.DeserializeObject<int[]>(response.Content);
            return new SmsResponseDto()
            {
                Message = response.ErrorMessage,
                StatusCode = (int)response.StatusCode,
                DeliveryCode = contentResult[1]
            };
        }
        

        public Task<SmsResponseDto> Send(string[] recipients, string message)
        {
            throw new NotImplementedException();
        }
        
        public async Task<DeliveryStatus[]> GetDelivery(int deliveryCode)
        {
            var client = new RestClient(URL);
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            request.AddParameter("undefined", "{\"op\" : \"delivery\"" +
                $",\"uname\" : \"{userName}\"" +
                $",\"pass\":  \"{password}\"" +
                $",\"uinqid\" : \"{deliveryCode}\"" +
                 "}"
                , ParameterType.RequestBody);

            IRestResponse response = await client.ExecuteAsync(request);

            var contentResult = JsonConvert.DeserializeObject<string[]>(response.Content);
            var statusResults = JsonConvert.DeserializeObject<string[]>(contentResult[1]);

            return statusResults!=null? GetDeliveryStatuses(statusResults):null;
        }
        private DeliveryStatus[] GetDeliveryStatuses(string[] statusResults)
        {
            var deliveryStatuses = new DeliveryStatus[statusResults.Length];
            for(int i = 0; i < statusResults.Length; i++)
            {
                var statusResultContent = statusResults[i].Split(':');
                deliveryStatuses[i] = new DeliveryStatus()
                {
                    PhoneNumber = StringHelper.NormalizeMobileNumber(statusResultContent[0]),
                    Status = statusResultContent[1]
                };
            }
            return deliveryStatuses;
        }

        public async Task<SmsResponseDto> SendPattern(string recipient, string message)
        {            
            var client = new RestClient(URL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");   
            
            request.AddParameter("undefined", "{\"op\" : \"pattern\"" +
                $",\"user\" : \"{userName}\"" +
                $",\"pass\":  \"{password}\"" +
                $",\"fromNum\" : \"{senderNumber}\"" +
                $",\"toNum\": \"{recipient}\"" +
                $",\"patternCode\": \"{patternCode}\"" +
                ",\"inputData\" : [{\"vercode\": \""+message+"\"}]}"
                , ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            var contentResult = JsonConvert.DeserializeObject<int>(response.Content);
            return new SmsResponseDto()
            {
                Message = response.ErrorMessage,
                StatusCode = (int)response.StatusCode,
                DeliveryCode = contentResult
            };
        }
    }    
}
