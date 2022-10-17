using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using FluentAssertions;
using MozaeekCore.Specs.Model;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MozaeekCore.Specs
{
    [Binding]
    public class UnProcessedRequestCreationSteps
    {
        private UnProcessedRequest request;
        private HttpResponseMessage responseHttpMessage;
        [Given(@"'(.*)' is a WorkWriter in system")]
        public void GivenIsAWorkWriterInSystem(string p0)
        {
            //test
        }

        [Given(@"he has entered the following information")]
        public void GivenHeHasEnteredTheFollowingInformation(Table table)
        {
            request = table.CreateInstance<UnProcessedRequest>();
        }

        [When(@"he submits the data")]
        public void WhenHeSubmitsTheData()
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    var json = JsonConvert.SerializeObject(request);
                    var content = new StringContent(json,
                        Encoding.UTF8,
                        "application/json");
                    responseHttpMessage = client.PostAsync("http://localhost:5000/api/UnProcessedRequest/create", content).Result;
                }
            }

        }

        [Then(@"the result should be an UnProcessedRequest with isProcessed False value")]
        public void ThenTheResultShouldBeAnUnProcessedRequestWithIsProcessedFalseValue()
        {
            var location = responseHttpMessage.Headers.Location;
            HttpResponseMessage lastResponse;
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                };
                using (var client = new HttpClient(httpClientHandler))
                {
                    lastResponse = client.GetAsync(location).Result;
                    string responseBody = lastResponse.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<UnProcessedRequestResult>(responseBody);
                    result.IsProcessed.Should().Be(false);
                    result.Title.Should().Be(request.Title);

                }
            }
        }
    }
}
