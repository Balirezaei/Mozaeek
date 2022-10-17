using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.MessagePublisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync(object @event);
    }
}
