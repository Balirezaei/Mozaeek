using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.RequestPrice;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.RequestPrice
{
    public class UpdateRequestPriceCommandHandler : IBaseAsyncCommandHandler<UpdateRequestPriceCommand, UpdateRequestPriceResult>
    {
        private readonly IRequestPriceRepository _requestPriceRepository;

        public UpdateRequestPriceCommandHandler(IRequestPriceRepository requestPriceRepository)
        {
            _requestPriceRepository = requestPriceRepository;
        }

        public async Task<UpdateRequestPriceResult> HandleAsync(UpdateRequestPriceCommand cmd)
        {
            var details = cmd.RequestIds.Select(m => new RequestPriceDetail(m, cmd.Id)).ToList();
            var requestPrice = await _requestPriceRepository.Find(cmd.Id);
            requestPrice.ResetDetails();
            requestPrice.AddDetails(details);
            requestPrice.Update(cmd.Title, cmd.StartDate, cmd.EndDate, cmd.PriceUnitId, cmd.PriceAmount);
            _requestPriceRepository.Update(requestPrice);
            return new UpdateRequestPriceResult()
            {
                Id = cmd.Id
            };
        }
    }
}