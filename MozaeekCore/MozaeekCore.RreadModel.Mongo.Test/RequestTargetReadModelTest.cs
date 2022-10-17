using System;
using MozaeekCore.Persistense.MongoDb;

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
        }

        public void Dispose()
        {
            Repository.RemoveDB();
        }

        public MongoRepository Repository { get; set; }

    }
}