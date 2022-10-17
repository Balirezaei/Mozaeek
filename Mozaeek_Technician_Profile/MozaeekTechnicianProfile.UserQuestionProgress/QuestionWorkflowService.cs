using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekTechnicianProfile.ApiCall;
using MozaeekTechnicianProfile.ApiCall.ResponseMessages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MozaeekTechnicianProfile.UserQuestionProgress
{
    public interface IQuestionWorkflowService
    {
        Task<List<UserQuestionForTechnicianProcess>> GetAllQuestionForTechnicianProcesses();
        Task<UpdateUserQuestionStateResult> UpdateQuestionState(UpdateUserQuestionStateInput input);
    }

    public class QuestionWorkflowService : IQuestionWorkflowService
    {
        private readonly IHttpClientFactory _clientFactory;
        public QuestionWorkflowService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<UserQuestionForTechnicianProcess>> GetAllQuestionForTechnicianProcesses()
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var bsClient = new HttpClientRequestor(client);
            HttpResponseMessage response = await client.GetAsync("QuestionWorkflow/GetAllQuestionForTechnicianProcesses");
            var serializerSettings = new JsonSerializerSettings();
            var resultContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var result = JsonConvert.DeserializeObject<Result<List<UserQuestionForTechnicianProcess>>>(resultContent, serializerSettings);
                if (result.Error != null)
                {
                    throw new Exception(result.Error.Message);
                }
                return result.Data;
            }
            return null;
        }

        public async Task<UpdateUserQuestionStateResult> UpdateQuestionState(UpdateUserQuestionStateInput input)
        {
            var client = _clientFactory.CreateClient("UserProfile");
            var json = JsonConvert.SerializeObject(input);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("QuestionWorkflow/UpdateQuestionState", httpContent);
            var resultContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var result = JsonConvert.DeserializeObject<Result<UpdateUserQuestionStateResult>>(resultContent, serializerSettings);
                if (result.Error != null)
                {
                    throw new Exception(result.Error.Message);
                }
                return result.Data;
            }
            return null;
            //var bsClient = new HttpClientRequestor(client);
            //var result = await bsClient.RequestPostAsync<Result<UpdateUserQuestionStateResult>>(input, "QuestionWorkflow/UpdateQuestionState");
            //if (result.Error != null)
            //{
            //    throw new Exception(result.Error.Message);
            //}
            //return result.Data;
        }
    }
}
