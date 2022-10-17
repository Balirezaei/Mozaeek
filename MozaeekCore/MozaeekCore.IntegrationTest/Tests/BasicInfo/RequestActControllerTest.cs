using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.RestAPI;
using Newtonsoft.Json;
using Xunit;

namespace MozaeekCore.IntegrationTest
{
    public class RequestActControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public RequestActControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task RequestAct_should_create_successfully()
        {
            var client = _factory.CreateClient();
            var title = "Test RequestAct Integration " + new Random().Next(1, 1000);
            var json = JsonConvert.SerializeObject(new CreateRequestActCommand() { Title = title });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/RequestAct/create", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var savedResponse = await client.GetAsync(response.Headers.Location.AbsolutePath.ToString());

            var responseText = await savedResponse.Content.ReadAsStringAsync();

            var savedobj = JsonConvert.DeserializeObject<Result<RequestActDto>>(responseText);

            savedobj.Data.Title.Should().Be(title);

            await client.DeleteAsync("/api/RequestAct/Delete?id=" + savedobj.Data.Id);
        }

        //[Fact]
        //public async Task RequestAct_should_GetAll_successfully()
        //{
        //    int preSavedRequestAct = 3;
        //    var client = _factory.CreateClient();

        //    var savedResponse = await client.GetAsync("/api/RequestAct/GetAll" + "?PageSize=20&PageNumber=1");

        //    var responseText = await savedResponse.Content.ReadAsStringAsync();

        //    var savedobj = JsonConvert.DeserializeObject<Result<PagedListResult<RequestActDto>>>(responseText);

        //    savedobj.Data.List.Count.Should().Be(preSavedRequestAct);
        //}

        // [Fact]
        // public async Task RequestAct_should_update_successfully()
        // {
        //     var client = _factory.CreateClient();
        //     var response = await client.GetAsync("/api/RequestAct/GetById/1");
        //
        //     var responseText = await response.Content.ReadAsStringAsync();
        //
        //     var obj = JsonConvert.DeserializeObject<Result<RequestActDto>>(responseText);
        //
        //     var newTitle = obj.Data.Title + " Updated!";
        //
        //     obj.Data.Title = newTitle;
        //
        //     var json = JsonConvert.SerializeObject(obj.Data);
        //     var content = new StringContent(json,
        //         Encoding.UTF8,
        //         "application/json");
        //
        //     var updatedResponse = await client.PutAsync("/api/RequestAct/update", content);
        //
        //     var updatedResponseText = await updatedResponse.Content.ReadAsStringAsync();
        //
        //     var newUpdated = JsonConvert.DeserializeObject<Result<RequestActDto>>(updatedResponseText);
        //     newUpdated.Data.Title.Should().Be(newTitle);
        // }


    }
}