using System.Threading.Tasks;
using MozaeekCore.RestAPI;
using Xunit;

namespace MozaeekCore.IntegrationTest.Tests.CQRSCompleteRound
{
    public class DefiniteRequestOrgCrudTest : IClassFixture<WebFactoryInMongoDb<Startup>>
    {
        private readonly WebFactoryInMongoDb<Startup> _factory;

        public DefiniteRequestOrgCrudTest(WebFactoryInMongoDb<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public Task DefiniteRequestOrgShouldCreateOnReadAndWriteModelSuccessfully()
        {
            var client = _factory.CreateClient();

            return Task.CompletedTask;
        }

            
    }
}