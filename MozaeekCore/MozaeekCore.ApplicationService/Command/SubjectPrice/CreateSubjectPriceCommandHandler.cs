using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.SubjectPrice
{
    public class CreateSubjectPriceCommandHandler : IBaseAsyncCommandHandler<CreateSubjectPriceCommand, CreateSubjectPriceResult>
    {
        private readonly ISubjectPriceRepository _subjectPriceRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PriceCurrency> _priceCurrencyRepository;

        public CreateSubjectPriceCommandHandler(ISubjectPriceRepository subjectPriceRepository, IMessagePublisher publisher, IUnitOfWork unitOfWork, IGenericRepository<PriceCurrency> priceCurrencyRepository)
        {
            _subjectPriceRepository = subjectPriceRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _priceCurrencyRepository = priceCurrencyRepository;
        }

        public async Task<CreateSubjectPriceResult> HandleAsync(CreateSubjectPriceCommand cmd)
        {
            var details = cmd.SubjectIds.Select(m => new SubjectPriceDetail(m)).ToList();
            var subjectPrice = new SubjectPriceHeader(0, cmd.Title, cmd.PriceUnitId, cmd.PriceAmount, cmd.SystemShare, cmd.StartDate, cmd.EndDate, details);
            var priceCurrency = await _priceCurrencyRepository.Find(cmd.PriceUnitId);
            await _subjectPriceRepository.CreateSubjectPrice(subjectPrice);
            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new SubjectPriceCreateOrUpdated(
                subjectPrice.Id,
                priceCurrency.Unit,
                priceCurrency.Id,
                subjectPrice.PriceAmount,
                subjectPrice.Title,
                subjectPrice.StartDate,
                subjectPrice.EndDate,
                subjectPrice.IsActive,
                subjectPrice.SystemShare,
                subjectPrice.TechnicianShare,
                details.Select(m => m.SubjectId).ToList(),
                true
            ));

            return new CreateSubjectPriceResult()
            {
                Id = subjectPrice.Id
            };
        }
    }
}