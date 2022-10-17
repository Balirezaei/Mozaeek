using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MozaeekUserProfile.OutBoxManagement
{
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
            return _context.OutboxMessages.FromSqlRaw(@"DECLARE @UpdatedIDs table (ID int)
                                                        UPDATE       TOP(10) OutboxMessage
                                                        SET           State = 2
                                                        OUTPUT inserted.Id
                                                        INTO    @UpdatedIDs
                                                        WHERE(State = 1)
                                                        SELECT        TOP(10) Id, Data, Type, EventId, EventDate, State, ModifiedDate
                                                        FROM            OutboxMessage
                                                        WHERE       Id IN(SELECT Id from @UpdatedIDs)").ToList();
            // return _context.OutboxMessages.Where(m => m.State == OutboxMessageState.ReadyToSend).ToList();
        }

        public Task SaveChange()
        {
            return _context.SaveChangesAsync();
        }
    }
}