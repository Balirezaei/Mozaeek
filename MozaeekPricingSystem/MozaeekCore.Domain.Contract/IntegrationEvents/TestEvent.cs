using MozaeekCore.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain.Contract.IntegrationEvents
{
    public class TestEvent:IEvent
    {
        public TestEvent(string message)
        {
            Message = message;
        }
        public string Message { get; private set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}
