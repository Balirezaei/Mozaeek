using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Pricing;

namespace MozaeekCore.ApplicationService.Command.SubjectPrice
{
    public class DeleteSubjectPriceCommandHandler : IBaseAsyncCommandHandler<DeleteSubjectPriceCommand, DeleteCommandResult>
    {
        private readonly ISubjectPriceRepository _subjectPriceRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubjectPriceCommandHandler(ISubjectPriceRepository subjectPriceRepository, IMessagePublisher publisher, IUnitOfWork unitOfWork)
        {
            _subjectPriceRepository = subjectPriceRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteSubjectPriceCommand cmd)
        {
            var subjectPrice = await _subjectPriceRepository.Find(cmd.Id);
            _subjectPriceRepository.Delete(subjectPrice);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new SubjectPriceDeleted(cmd.Id));
            return new DeleteCommandResult();
        }

    }
}