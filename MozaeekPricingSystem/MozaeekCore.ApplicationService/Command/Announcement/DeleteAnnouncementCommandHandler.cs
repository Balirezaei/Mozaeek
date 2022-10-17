using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteAnnouncementCommandHandler : IBaseAsyncCommandHandler<DeleteAnnouncementCommand, DeleteCommandResult>
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteAnnouncementCommandHandler(IAnnouncementRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteAnnouncementCommand cmd)
        {
            var Announcement = await _repository.Find(cmd.Id);
            _repository.Delete(Announcement);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new AnnouncementDeleted(cmd.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}