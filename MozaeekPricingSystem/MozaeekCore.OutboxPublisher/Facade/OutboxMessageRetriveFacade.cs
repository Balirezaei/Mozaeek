using System;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.OutBoxManagement;

namespace RssRetriveProcess.Facade
{
    public interface IOutboxMessageRetriveFacade
    {
        Task SendOutboxMessageToQue();
    }

    public class OutboxMessageRetriveFacade : IOutboxMessageRetriveFacade
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly IOutboxRepository _outboxRepository;

        public OutboxMessageRetriveFacade(IMessagePublisher messagePublisher, IOutboxRepository outboxRepository)
        {
            _messagePublisher = messagePublisher;
            _outboxRepository = outboxRepository;
        }

        public async Task SendOutboxMessageToQue()
        {
            try
            {
                var readyToSend = _outboxRepository.GetAllReadyToSend();
                foreach (var message in readyToSend)
                {
                    var eventMessage = message.RecreateMessage();
                    Console.WriteLine("Before" + message.Data + " -- " + message.EventDate);
                    await _messagePublisher.PublishAsync(eventMessage);
                    Console.WriteLine("After" + message.Data + " -- " + message.EventDate);
                    message.ChangeState(OutboxMessageState.SendToQue);
                    _outboxRepository.UpdateOutboxMesageSatate(message.EventId, OutboxMessageState.SendToQue);
                }
                await _outboxRepository.SaveChange();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}