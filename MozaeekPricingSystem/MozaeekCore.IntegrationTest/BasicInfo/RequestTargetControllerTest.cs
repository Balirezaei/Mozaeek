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
    public class RequestTargetControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public RequestTargetControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        // [Fact]
        // public async Task RequestTarget_should_create_successfully()
        // {
        //     var client = _factory.CreateClient();
        //     var title = "Test RequestTarget Integration " + new Random().Next(1, 1000);
        //     var json = JsonConvert.SerializeObject(new CreateRequestTargetCommand() { Title = title });
        //
        //     var content = new StringContent(json,
        //         Encoding.UTF8,
        //         "application/json");
        //
        //     var response = await client.PostAsync("/api/RequestTarget/create", content);
        //
        //     response.StatusCode.Should().Be(HttpStatusCode.Created);
        //
        //   //  var savedRequestTargetResponse = await client.GetAsync(response.Headers.Location.AbsolutePath.ToString());
        //
        //     var savedResponse = await client.GetAsync(response.Headers.Location.AbsolutePath.ToString());
        //
        //     var responseText = await savedResponse.Content.ReadAsStringAsync();
        //
        //     var savedobj = JsonConvert.DeserializeObject<Result<RequestTargetDto>>(responseText);
        //
        //     savedobj.Data.Title.Should().Be(title);
        //
        //     await client.DeleteAsync("/api/RequestTarget/Delete?id=" + savedobj.Data.Id);
        // }


        // [Fact]
        // public async Task RequestTarget_should_update_successfully()
        // {
        //     var client = _factory.CreateClient();
        //     var response = await client.GetAsync("/api/RequestTarget/GetById/1");
        //
        //     var responseText = await response.Content.ReadAsStringAsync();
        //
        //     var obj = JsonConvert.DeserializeObject<Result<RequestTargetDto>>(responseText);
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
        //     var updatedResponse = await client.PutAsync("/api/RequestTarget/update", content);
        //     
        //     var updatedResponseText = await updatedResponse.Content.ReadAsStringAsync();
        //     
        //     var newUpdated = JsonConvert.DeserializeObject<Result<RequestTargetDto>>(updatedResponseText);
        //     newUpdated.Data.Title.Should().Be(newTitle);
        // }
    }
}