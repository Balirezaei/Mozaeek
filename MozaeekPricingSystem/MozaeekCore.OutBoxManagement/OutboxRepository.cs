using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Core.MessagePublisher;

namespace MozaeekCore.OutBoxManagement
{
    public interface IOutboxRepository
    {
        void CreateOutboxMessage(OutboxMessage outboxMessage);
        void UpdateOutboxMesageSatate(Guid eventId, OutboxMessageState state);
        List<OutboxMessage> GetAllReadyToSend();
        Task SaveChange();

    }

    public class OutboxRepository : IOutboxRepository
    {
        private readonly EventStoreContext _context;

        public OutboxRepository(EventStoreContext context)
        {
            _context = context;
        }

        public void CreateOutboxMessage(OutboxMessage outboxMessage)
        {
            var test = _context.Database.GetDbConnection().ConnectionString;

            _context.OutboxMessages.Add(outboxMessage);
        }

        public void UpdateOutboxMesageSatate(Guid eventId, OutboxMessageState state)
        {
            var outbox = _context.OutboxMessages.FirstOrDefault(m => m.EventId == eventId);
            outbox.ChangeState(state);
        }

        public List<OutboxMessage> GetAllReadyToSend()
        {
            return _context.OutboxMessages.Where(m => m.State == OutboxMessageState.ReadyToSend).ToList();
        }

        public Task SaveChange()
        {
            return _context.SaveChangesAsync();
        }
    }
}