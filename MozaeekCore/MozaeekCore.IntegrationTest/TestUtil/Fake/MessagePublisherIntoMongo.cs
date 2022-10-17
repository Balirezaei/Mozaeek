using System.Threading.Tasks;
using MozaeekCore.Core.MessagePublisher;

namespace MozaeekCore.IntegrationTest.TestUtil.Fake
{
    public class MessagePublisherIntoMongo : IMessagePublisher
    {
        public PublisherType Key { get; }
        public Task PublishAsync(object @event)
        {
            var typeOfEvent = @event.GetType();
            return Task.CompletedTask;
        }
    }
}