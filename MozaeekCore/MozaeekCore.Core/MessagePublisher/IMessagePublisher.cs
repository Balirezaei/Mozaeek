using System.Threading.Tasks;

namespace MozaeekCore.Core.MessagePublisher
{
    public interface IMessagePublisher
    {
        PublisherType Key { get; }
        Task PublishAsync(object @event);
    }
}
