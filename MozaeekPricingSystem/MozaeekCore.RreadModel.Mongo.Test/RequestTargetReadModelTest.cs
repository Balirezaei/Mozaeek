using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;
using MozaeekCore.RreadModel.Mongo.Test.Builder;
using Xunit;

namespace MozaeekCore.RreadModel.Mongo.Test
{
    public class RequestTargetReadModelTest : IDisposable
    {
        private RequestTargetQueryService _tempRequestTargetQueryService;

        public RequestTargetReadModelTest()
        {
            Repository = new MongoRepository(new ReadModelDatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestReadDb" + new Random().Next(1, 1000)
            });
            _tempRequestTargetQueryService = new RequestTargetQueryService(Repository);
        }

        public void Dispose()
        {
            Repository.RemoveDB();
        }
        public MongoRepository Repository { get; set; }

        [Fact]
        public async void Should_Create_RequestTarget_On_Mongo_Successfully()
        {
            //
            // var objectToSave = new RequestTargetQuery(1, "test", new List<LabelQuery>(), new List<SubjectQuery>(), new List<RequestOrgQuery>(), DateTime.Now, Guid.NewGuid());
            //
            // await _requestTargetQueryService.Create(objectToSave);
            //
            // var savedObject = await _requestTargetQueryService.Get(objectToSave.Id);
            //
            // savedObject.Title.Should().Be(objectToSave.Title);
        }



        [Fact]
        public async void Should_Delete_Saved_Label()
        {
            // var objectToSave = new RequestTargetQuery(1, "test", new List<LabelQuery>(), new List<SubjectQuery>(), new List<RequestOrgQuery>(), DateTime.Now, Guid.NewGuid());
            // await _requestTargetQueryService.Create(objectToSave);
            // await _requestTargetQueryService.Remove(objectToSave.Id);
            // var savedObject = await _requestTargetQueryService.Get(objectToSave.Id);
            // savedObject.Should().Be(null);
        }

    }
}