// using System.Threading.Tasks;
// using MozaeekCore.Core.Events;
// using MozaeekCore.Core.MessagePublisher;
// using MozaeekCore.OutBoxManagement;
//
// namespace MozaeekCore.OutboxPublisherService.Service
// {
//     public interface IOutBoxChangeSatateService
//     {
//         Task ChangeState(IEvent domainEvent);
//     }
//
//     public class OutBoxChangeSatateService : IOutBoxChangeSatateService
//     {
//         private readonly IOutboxRepository _outboxRepository;
//
//         public OutBoxChangeSatateService(IOutboxRepository outboxRepository)
//         {
//             _outboxRepository = outboxRepository;
//         }
//
//         public Task ChangeState(IEvent domainEvent)
//         {
//             _outboxRepository.UpdateOutboxMesageSatate(domainEvent.EventId, OutboxMessageState.SendToQue);
//             return _outboxRepository.SaveChange();
//         }
//     }
// }