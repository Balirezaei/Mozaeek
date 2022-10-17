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
    public class UpdateSubjectPriceCommandHandler : IBaseAsyncCommandHandler<UpdateSubjectPriceCommand, UpdateSubjectPriceResult>
    {
        private readonly ISubjectPriceRepository _subjectPriceRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PriceCurrency> _priceCurrencyIdRepository;
        public UpdateSubjectPriceCommandHandler(ISubjectPriceRepository subjectPriceRepository, IMessagePublisher publisher, IUnitOfWork unitOfWork, IGenericRepository<PriceCurrency> priceCurrencyIdRepository)
        {
            _subjectPriceRepository = subjectPriceRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _priceCurrencyIdRepository = priceCurrencyIdRepository;
        }

        public async Task<UpdateSubjectPriceResult> HandleAsync(UpdateSubjectPriceCommand cmd)
        {
            var details = cmd.SubjectIds.Select(m => new SubjectPriceDetail(m, cmd.Id)).ToList();
            var subjectPrice = await _subjectPriceRepository.Find(cmd.Id);
            var priceCurrency = await _priceCurrencyIdRepository.Find(cmd.PriceCurrencyId);

            subjectPrice.ResetDetails();
            subjectPrice.AddDetails(details);
            subjectPrice.Update(cmd.Title, cmd.StartDate, cmd.EndDate, cmd.PriceCurrencyId, cmd.PriceAmount, cmd.SystemShare);

            _subjectPriceRepository.Update(subjectPrice);

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
                false
            ));

            return new UpdateSubjectPriceResult()
            {
                Id = cmd.Id
            };
        }
    }
}