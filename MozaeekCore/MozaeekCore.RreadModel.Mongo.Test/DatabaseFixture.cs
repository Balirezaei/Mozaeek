using System;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.RreadModel.Mongo.Test
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Repository = new MongoRepository(new ReadModelDatabaseSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestReadDb"+new Random().Next(1,1000)
            });
        }

        public void Dispose()
        {
            Repository.RemoveDB();
            // ... clean up test data from the database ...
        }
        public MongoRepository Repository { get; set; }
    }
}
