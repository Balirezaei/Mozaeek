using System;
using FluentAssertions;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.RreadModel.Mongo.Test.Builder;
using Xunit;

namespace MozaeekCore.RreadModel.Mongo.Test
{
    public class AnnouncementQueryModelTest : IDisposable
    {
        private AnnouncementQueryService _announcementQueryService;

        public AnnouncementQueryModelTest()
        {
            Repository = new MongoRepository(new ReadModelDatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestReadDb" + new Random().Next(1, 1000)
            });
        }

        public void Dispose()
        {
            Repository.RemoveDB();
        }
        public MongoRepository Repository { get; set; }





    }
}