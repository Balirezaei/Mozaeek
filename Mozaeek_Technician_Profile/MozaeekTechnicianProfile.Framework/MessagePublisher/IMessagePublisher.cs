using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MozaeekTechnicianProfile.Core.Core.Events;

namespace MozaeekTechnicianProfile.Core.Core.MessagePublisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync(DomainEvent @event);
    }
}
