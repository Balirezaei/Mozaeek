using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MozaeekTechnicianProfile.ApiCall
{
    public class HttpClientRequestor
    {
        private readonly HttpClient _httpClient;

        public HttpClientRequestor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RS> RequestPostAsync<RS>(object data, string requestUrl)
        {
            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, httpContent);
            return await CreateRequestResult<RS>(response, json);
        }

        public async Task<RS> RequestPutAsync<RS>(object data, string requestUrl)
        {
            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(requestUrl, httpContent);
            return await CreateRequestResult<RS>(response, json);
        }

        public async Task<RS> RequestDeleteAsync<RS>(string requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl);
            return await CreateRequestResult<RS>(response, requestUrl);
        }


        public async Task<RS> RequestGetAsync<RS>(string requestUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            var result = await response.Content.ReadAsStringAsync();
            //response.EnsureSuccessStatusCode();
            return await CreateRequestResult<RS>(response, requestUrl);
        }

        private async Task<RS> CreateRequestResult<RS>(HttpResponseMessage response, string requestContent)
        {
            var serializerSettings = new JsonSerializerSettings();
            var resultContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                return JsonConvert.DeserializeObject<RS>(resultContent, serializerSettings);
            }
            else
            {
                return JsonConvert.DeserializeObject<RS>(resultContent, serializerSettings);
            }


        }
    }
}