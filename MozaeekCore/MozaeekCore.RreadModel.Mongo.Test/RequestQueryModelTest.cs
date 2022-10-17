using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
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
        private IBasicInfoQueryService _basicInfoQueryService;
        public RequestQueryModelTest()
        {
            Repository = new MongoRepository(new ReadModelDatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestReadDb" + new Random().Next(1, 1000)
            });
            _basicInfoQueryService = new BasicInfoQueryService(Repository);
            _requestQueryService = new RequestQueryService(Repository, _basicInfoQueryService);
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
        public async Task TestPredicateContain()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);
            var builder = Builders<RequestQuery>.Filter;
            var filter = builder.Regex("Title", new BsonRegularExpression(".*سند.*"));
            var res = (await Repository.RequestQueryCollection.FindAsync(filter)).FirstOrDefault();
            res.Title.Should().Contain("اخذ");
        }

        [Fact]
        public async Task TestPredicateIdEqual()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);
            var res = (await Repository.RequestQueryCollection.FindAsync(m => m.Id == 1)).FirstOrDefault();
            res.Id.Should().Be(1);
        }

        [Fact]
        public async Task TestPredicateEaualComplexProperty()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);
            var builder = Builders<RequestQuery>.Filter;
            var filter = builder.Eq("RequestTarget.Id", 1);
            var res = (await Repository.RequestQueryCollection.FindAsync(filter)).FirstOrDefault();
            res.RequestTarget.Id.Should().Be(1);
        }

        [Fact]
        public async Task TestPredicateBooleanEqual()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);
            var builder = Builders<RequestQuery>.Filter;
            var filter = builder.Eq("FullOnline", true);
            var res = (await Repository.RequestQueryCollection.FindAsync(filter)).FirstOrDefault();

            res.Id.Should().Be(1);
        }

        [Fact]
        public async Task TestPredicateIdInList()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);
            var Ids = new[] { 1, 2, 3 };

            var res = await _requestQueryService
                .GetByPredicate(m => Ids.Any(z => z == m.Id));

           //   = (await Repository.RequestQueryCollection.FindAsync(m => m.Id == 1)).FirstOrDefault();
            res.First().Id.Should().Be(1);
        }

        [Fact]
        public async Task TestPredicateDynamicEqual()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);

            var builder = Builders<RequestQuery>.Filter;
            IList<FilterDefinition<RequestQuery>> filters = new List<FilterDefinition<RequestQuery>>();
            object idValue = (long)1;
                      filters.Add(builder.Eq("Id", idValue));

                      var filterConcat = builder.And(filters);
            var res = (await Repository.RequestQueryCollection.FindAsync(filterConcat)).FirstOrDefault();
            res.Id.Should().Be((long)idValue);
        }

        [Fact]
        public async Task TestPredicateDynamicNotEqual()
        {
            var req = new RequestBuilder().BuildSampleParameter();
            await _requestQueryService.Create(req);

            var builder = Builders<RequestQuery>.Filter;
            IList<FilterDefinition<RequestQuery>> filters = new List<FilterDefinition<RequestQuery>>();
            object idValue = (long)2;
            filters.Add(builder.Ne("Id", idValue));

            var filterConcat = builder.And(filters);
            var res = (await Repository.RequestQueryCollection.FindAsync(filterConcat)).FirstOrDefault();
            res.Id.Should().NotBe(null);
        }
    }
}