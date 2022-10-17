using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.RequestPrice
{
    public class CreateRequestPriceCommandHandler : IBaseAsyncCommandHandler<CreateRequestPriceCommand, CreateRequestPriceResult>
    {
        private readonly IRequestPriceRepository _requestPriceRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PriceCurrency> _priceUnitRepository;

        public CreateRequestPriceCommandHandler(IRequestPriceRepository requestPriceRepository, IMessagePublisher publisher, IUnitOfWork unitOfWork, IGenericRepository<PriceCurrency> priceUnitRepository)
        {
            _requestPriceRepository = requestPriceRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _priceUnitRepository = priceUnitRepository;
        }

        public async Task<CreateRequestPriceResult> HandleAsync(CreateRequestPriceCommand cmd)
        {
            var details = cmd.RequestIds.Select(m => new RequestPriceDetail(m)).ToList();
            var requestPrice = new RequestPriceHeader(0, cmd.Title, cmd.PriceUnitId, cmd.PriceAmount, cmd.SystemShare, cmd.StartDate, cmd.EndDate, details);
            var priceUnit = await _priceUnitRepository.Find(cmd.PriceUnitId);
            
            await _requestPriceRepository.CreateRequestPrice(requestPrice);
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
                true
            ));

            return new CreateRequestPriceResult()
            {
                Id = requestPrice.Id
            };
        }
    }
}