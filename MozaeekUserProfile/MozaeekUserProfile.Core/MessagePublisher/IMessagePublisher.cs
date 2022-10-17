using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Core.MessagePublisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync(object @event);
    }
}
