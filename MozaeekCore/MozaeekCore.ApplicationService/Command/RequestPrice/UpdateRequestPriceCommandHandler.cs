using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.RequestPrice
{
    public class UpdateRequestPriceCommandHandler : IBaseAsyncCommandHandler<UpdateRequestPriceCommand, UpdateRequestPriceResult>
    {
        private readonly IRequestPriceRepository _requestPriceRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PriceCurrency> _priceUnitRepository;

        public UpdateRequestPriceCommandHandler(IRequestPriceRepository requestPriceRepository, IMessagePublisher publisher, IUnitOfWork unitOfWork, IGenericRepository<PriceCurrency> priceUnitRepository)
        {
            _requestPriceRepository = requestPriceRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _priceUnitRepository = priceUnitRepository;
        }

        public async Task<UpdateRequestPriceResult> HandleAsync(UpdateRequestPriceCommand cmd)
        {
            var details = cmd.RequestIds.Select(m => new RequestPriceDetail( cmd.Id,m)).ToList();
            var requestPrice = await _requestPriceRepository.Find(cmd.Id);
            var priceUnit = await _priceUnitRepository.Find(cmd.PriceUnitId);
            requestPrice.ResetDetails();
            requestPrice.AddDetails(details);
            requestPrice.Update(cmd.Title, cmd.StartDate, cmd.EndDate, cmd.PriceUnitId, cmd.PriceAmount, cmd.SystemShare);

            _requestPriceRepository.Update(requestPrice);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestPriceCreateOrUpdated(
                requestPrice.Id,
                priceUnit.Unit,
                priceUnit.Id,
                requestPrice.PriceAmount,
                requestPrice.Title,
                requestPrice.StartDate,
                requestPrice.EndDate,
                requestPrice.IsActive,
                requestPrice.SystemShare,
                requestPrice.TechnicianShare,
                details.Select(m => m.RequestId).ToList(),
                false
            ));

            return new UpdateRequestPriceResult()
            {
                Id = cmd.Id
            };
        }
    }
}