using System;

namespace MozaeekCore.Core.MessagePublisher
{
    public enum OutboxMessageState
    {
        ReadyToSend = 1,
        SendToQue = 2,
        Completed = 3,
        ErrorOnAddToQueue = 4,
        ErrorOnConsume = 5
    }
}
