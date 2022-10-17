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
    public class LabelControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public LabelControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Label_should_create_successfully()
        {
            var client = _factory.CreateClient();
            var title = "Test Label Integration " + new Random().Next(1, 1000);
            var json = JsonConvert.SerializeObject(new CreateLabelCommand() { Title = title });

            var content = new StringContent(json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/api/label/create", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var savedLabelResponse = await client.GetAsync(response.Headers.Location.AbsolutePath.ToString());

            var responseText = await savedLabelResponse.Content.ReadAsStringAsync();

            var savedLabel = JsonConvert.DeserializeObject<Result<LabelDto>>(responseText);

            savedLabel.Data.Title.Should().Be(title);

        }

        // [Fact]
        // public async Task label_Get_All_Method_Should_Retun_Successfully()
        // {
        //     var client = _factory.CreateClient();
        //
        //     var title = "Test Label Integration " + new Random().Next(1, 1000);
        //
        //     var json = JsonConvert.SerializeObject(new CreateLabelCommand() { Title = title });
        //     await client.PostAsync("/api/label/create", new StringContent(json, Encoding.UTF8, "application/json"));
        //     
        //     var query = new LabelFilterContract() { PageSize = 10, PageNumber = 1 };
        //     var url = "/api/label/GetAllParent?" + query.ToQueryString();
        //
        //     var response = await client.GetAsync(url);
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);
        //
        //     var responseText = await response.Content.ReadAsStringAsync();
        //
        //     var labels = JsonConvert.DeserializeObject<Result<PagedListResult<LabelDto>>>(responseText);
        //
        //     labels.Data.List.FirstOrDefault().ParentId.Should().BeNull();
        // }
    }
}