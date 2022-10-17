using System;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.OutboxPublisherService.Service;
using Newtonsoft.Json;

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
   

        public OutboxMessageRetriveFacade(IMessagePublisher messagePublisher, IOutboxRepository outboxRepository, ServiceInformation serviceInformation)
        {
            _messagePublisher = messagePublisher;
            _outboxRepository = outboxRepository;
        }

        public async Task SendOutboxMessageToQue()
        {
            try
            {
                // Console.WriteLine("Before readyToSend");

                var readyToSend = _outboxRepository.GetAllReadyToSend();
                if (readyToSend.Any())
                {
                    Console.WriteLine(JsonConvert.SerializeObject(readyToSend));
                }
                foreach (var message in readyToSend)
                {
                    try
                    {
                        var eventMessage = message.RecreateMessage();
                        Console.WriteLine("Before" + message.Data + " -- " + message.EventDate);
                        await _messagePublisher.PublishAsync(eventMessage);
                        Console.WriteLine("After" + message.Data + " -- " + message.EventDate);
                        // _outboxRepository.UpdateOutboxMesageSatate(message.EventId, OutboxMessageState.SendToQue);
                    }
                    catch (Exception e)
                    {
                        message.ChangeState(OutboxMessageState.ErrorOnAddToQueue);
                    }

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            await _outboxRepository.SaveChange();
        }
    }
}