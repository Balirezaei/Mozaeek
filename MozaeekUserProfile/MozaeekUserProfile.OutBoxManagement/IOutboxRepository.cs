using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MozaeekUserProfile.OutBoxManagement
{
    public interface IOutboxRepository
    {
        void CreateOutboxMessage(OutboxMessage outboxMessage);
        void UpdateOutboxMesageSatate(Guid eventId, OutboxMessageState state);
        List<OutboxMessage> GetAllReadyToSend();
        Task SaveChange();

    }
}