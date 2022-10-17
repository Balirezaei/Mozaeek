using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MozaeekCore.ApplicationService.Contract.RequestPrice;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.RequestPrice
{
    public class CreateRequestPriceCommandHandler : IBaseAsyncCommandHandler<CreateRequestPriceCommand, CreateRequestPriceResult>
    {
        private readonly IRequestPriceRepository _requestPriceRepository;

        public CreateRequestPriceCommandHandler(IRequestPriceRepository requestPriceRepository)
        {
            _requestPriceRepository = requestPriceRepository;
        }

        public async Task<CreateRequestPriceResult> HandleAsync(CreateRequestPriceCommand cmd)
        {
            var details = cmd.RequestIds.Select(m => new RequestPriceDetail(m)).ToList();
            var requestPrice = new RequestPriceHeader(0, cmd.Title, cmd.PriceUnitId, cmd.PriceAmount, cmd.StartDate, cmd.EndDate, details);
            await _requestPriceRepository.CreateRequestPrice(requestPrice);
            return new CreateRequestPriceResult()
            {
                Id = requestPrice.Id
            };
        }
    }
}