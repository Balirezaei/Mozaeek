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
    public class RequestOrgControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public RequestOrgControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task RequestOrg_should_create_successfully()
        {
            var client = _factory.CreateClient();
            var title = "Test RequestOrg Integration " + new Random().Next(1, 1000);
            var json = JsonConvert.SerializeObject(new CreateRequestOrgCommand() { Title = title });

            var content = new StringContent(json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/RequestOrg/create", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            
            var savedResponse = await client.GetAsync(response.Headers.Location.AbsolutePath.ToString());

            var responseText = await savedResponse.Content.ReadAsStringAsync();

            var savedobj = JsonConvert.DeserializeObject<Result<RequestOrgDto>>(responseText);

            savedobj.Data.Title.Should().Be(title);

            await client.DeleteAsync("/api/RequestOrg/Delete?id=" + savedobj.Data.Id);
        }
        //[Fact]
        //public async Task RequestOrg_should_GetAllParent_successfully()
        //{
        //    int preSavedRequestOrg = 2;
        //    var client = _factory.CreateClient();

        //    var getAllResponse = await client.GetAsync("/api/RequestOrg/GetAllParent" + "?PageSize=20&PageNumber=1");

        //    var responseText = await getAllResponse.Content.ReadAsStringAsync();

        //    getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var savedobj = JsonConvert.DeserializeObject<Result<PagedListResult<RequestOrgDto>>>(responseText);

        //    savedobj.Data.List.Count.Should().Be(preSavedRequestOrg);

        //}

        //[Fact]
        //public async Task RequestOrg_should_update_successfully()
        //{
        //    var client = _factory.CreateClient();
        //    var response = await client.GetAsync("/api/RequestOrg/GetById/1");

        //    var responseText = await response.Content.ReadAsStringAsync();

        //    var obj = JsonConvert.DeserializeObject<Result<RequestOrgDto>>(responseText);

        //    var newTitle = obj.Data.Title + " Updated!";

        //    obj.Data.Title = newTitle;

        //    var json = JsonConvert.SerializeObject(obj.Data);
        //    var content = new StringContent(json,
        //        Encoding.UTF8,
        //        "application/json");

        //    var updatedResponse = await client.PutAsync("/api/RequestOrg/update", content);

        //    var updatedResponseText = await updatedResponse.Content.ReadAsStringAsync();

        //    var newUpdated = JsonConvert.DeserializeObject<Result<RequestOrgDto>>(updatedResponseText);
        //    newUpdated.Data.Title.Should().Be(newTitle);
        //}


    }
}