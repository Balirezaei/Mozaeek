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
    public class LabelReadModelTest : IDisposable
    {
        private LabelQueryService _labelQueryService;
        private RequestTargetQueryService _requestTargetQueryService;

        public LabelReadModelTest()
        {
            Repository = new MongoRepository(new ReadModelDatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestReadDb"
            });
            Repository.RemoveDB();
            _labelQueryService = new LabelQueryService(Repository, new RequestTargetQueryService(Repository));
            _requestTargetQueryService=new RequestTargetQueryService(Repository);
        }

        public void Dispose()
        {
            Repository.RemoveDB();
        }
        public MongoRepository Repository { get; set; }



        [Fact]
        public async void Should_Create_Label_On_Mongo_Successfully()
        {
            var builder = new LabelBuilder();
            var objectToSave = builder.BuildFirstLevel();

            await _labelQueryService.Create(objectToSave);

            var savedObject = await _labelQueryService.Get(objectToSave.Id);

            savedObject.Title.Should().Be(objectToSave.Title);
        }

        [Fact]
        public async void Should_Create_Label_With_Parent_Successfully()
        {
            var builder = new LabelBuilder();
            var firstObjectToSave = builder.BuildFirstLevel();

            await _labelQueryService.Create(firstObjectToSave);

            var secondObjectToSave = builder.BuildSecondLevel();

            await _labelQueryService.Create(secondObjectToSave);

            var savedFirstObject = await _labelQueryService.Get(firstObjectToSave.Id);

            var savedSecondObject = await _labelQueryService.Get(secondObjectToSave.Id);

            savedFirstObject.HasChild.Should().Be(true);
            // savedSecondObject.Parents.Count.Should().Be(1);

            // savedSecondObject.Parents.First().Title.Should().Be(firstObjectToSave.Title);

        }

       




        [Fact]
        public async void Should_Delete_Saved_Label()
        {
            var builder = new LabelBuilder();
            var objectToSave = builder.BuildFirstLevel();
            await _labelQueryService.Create(objectToSave);
            await _labelQueryService.Remove(objectToSave.Id);
            var savedObject = await _labelQueryService.Get(objectToSave.Id);
            savedObject.Should().Be(null);
        }

        [Fact]
        public async void Should_Delete_With_Parent_Last_tree_On_Delete_Successfully()
        {
            var builder = new LabelBuilder();
            var firstObjectToSave = builder.BuildFirstLevel();
            await _labelQueryService.Create(firstObjectToSave);

            var secondObjectToSave = builder.BuildSecondLevel();
            await _labelQueryService.Create(secondObjectToSave);

            var thirdObjectToSave = builder.BuildThirdLevel();

            await _labelQueryService.Create(thirdObjectToSave);

            await _labelQueryService.Remove(thirdObjectToSave.Id);

            var savedSecond = await _labelQueryService.Get(secondObjectToSave.Id);

            savedSecond.HasChild.Should().Be(false);
        }

        [Fact]
        public async void Should_Update_RequestTargetLable_On_lable_Update()
        {
            var builder = new LabelBuilder();
            var firstObjectToSave = builder.BuildFirstLevel();
            await _labelQueryService.Create(firstObjectToSave);

            var target = new RequestTargetBuilder().BuildSampleTargetQueryWithLableAndRequestOrg();

            await Repository.RequestTargetQueryCollection.InsertOneAsync(target);
            firstObjectToSave.Title = firstObjectToSave.Title + "Updated";
            await _labelQueryService.Update(firstObjectToSave);
            var newTarget = await _requestTargetQueryService.Get(target.Id);
            newTarget.LabelList.FirstOrDefault().Title.Should().Be(firstObjectToSave.Title);
        }

    }
}