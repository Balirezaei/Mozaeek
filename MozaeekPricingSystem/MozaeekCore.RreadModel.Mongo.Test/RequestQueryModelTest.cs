using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;
using MozaeekCore.RreadModel.Mongo.Test.Builder;
using Xunit;

namespace MozaeekCore.RreadModel.Mongo.Test
{
    public class RequestQueryModelTest : IDisposable
    {
        private IRequestQueryService _requestQueryService;
        private IRequestTargetQueryService _requestTargetQueryService;

        public RequestQueryModelTest()
        {
            Repository = new MongoRepository(new ReadModelDatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestReadDb" + new Random().Next(1, 1000)
            });
            _requestQueryService = new RequestQueryService(Repository);
            _requestTargetQueryService = new RequestTargetQueryService(Repository);

            PrerequisiteForRunnigTest();
        }

        private string targetTitle = "سند تک برگ مسکونی";
        private string act = "اخذ";
        public async void PrerequisiteForRunnigTest()
        {
            var requestAct = new RequestActQuery(1, act, DateTime.Now, Guid.NewGuid());
            Repository.RequestActQueryCollection.InsertOne(requestAct);

            var org = new RequestOrgBuilder().BuildFirstLevel();
            Repository.RequestOrgQueryCollection.InsertOne(org);

            var label = new LabelBuilder().BuildFirstLevel();
            Repository.LabelQueryCollection.InsertOne(label);

            var p = new RequestTargetBuilder()
                .WithTitle(targetTitle)
                .BuildSampleTargetQueryWithLableAndRequestOrg();
            Repository.RequestTargetQueryCollection.InsertOne(p);
        }

        public MongoRepository Repository { get; set; }

        public void Dispose()
        {
            Repository.RemoveDB();
        }

        [Fact]
        public async Task Create_Request_Should_besuccessfully()
        {
            var expected = $"{act} {targetTitle}";

            var parameter = new RequestParameter(1, 1, 1,
                new List<RequestQueryDependency>() { new RequestQueryDependency("documents", 1) },
                new List<RequestQueryDependency>() { new RequestQueryDependency("nessesities", 1) },
                new List<RequestQueryDependency>() { new RequestQueryDependency("actions", 1) },
                new List<RequestQueryDependency>() { new RequestQueryDependency("qualificationIds", 1) }, new List<long>()
            );
            await _requestQueryService.Create(parameter);
            var actual = await _requestQueryService.Get(1);

            actual.Title.Should().Be(expected);
        }
        [Fact]
        public async Task Request_Should_Update_onRequestAct_update()
        {
            var expectedTarget = $"{targetTitle}" + " Updated";


            var parameter = new RequestBuilder().BuildSampleParameter();

            await _requestQueryService.Create(parameter);


            var preTarget = await _requestTargetQueryService.Get(1);

            var requestTargetParameter = new RequestTargetBuilder().WithTitle(expectedTarget).BuildSampleParameter();

            await _requestTargetQueryService.Update(requestTargetParameter);


            var actual = await _requestQueryService.Get(1);
            actual.RequestTarget.Title.Should().Be(expectedTarget);
        }
    }
}