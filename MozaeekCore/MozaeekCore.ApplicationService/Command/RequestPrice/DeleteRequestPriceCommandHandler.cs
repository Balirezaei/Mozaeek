using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.RequestPrice
{
    public class DeleteRequestPriceCommandHandler : IBaseAsyncCommandHandler<DeleteRequestPriceCommand, DeleteCommandResult>
    {
        private readonly IRequestPriceRepository _requestPriceRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRequestPriceCommandHandler(IRequestPriceRepository requestPriceRepository, IMessagePublisher publisher, IUnitOfWork unitOfWork)
        {
            _requestPriceRepository = requestPriceRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteRequestPriceCommand cmd)
        {
            var requestPrice = await _requestPriceRepository.Find(cmd.Id);
            _requestPriceRepository.Delete(requestPrice);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestPriceDeleted(cmd.Id));
            return new DeleteCommandResult();
        }
    }
}