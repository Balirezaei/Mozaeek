using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MozaeekUserProfile.Core.Core.Events;

namespace MozaeekUserProfile.Core.Core.MessagePublisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync(object @event);
    }
}
