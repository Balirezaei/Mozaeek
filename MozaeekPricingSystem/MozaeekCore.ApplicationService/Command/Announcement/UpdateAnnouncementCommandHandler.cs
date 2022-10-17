using System.Linq;
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
    public class UpdateAnnouncementCommandHandler : IBaseAsyncCommandHandler<UpdateAnnouncementCommand, UpdateAnnouncementCommandResult>
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateAnnouncementCommandHandler(IAnnouncementRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdateAnnouncementCommandResult> HandleAsync(UpdateAnnouncementCommand cmd)
        {
            var announcementPoints = cmd.Points.Select(m => m.Id).Select(m => new AnnouncementPoint()
            {
                PointId = m
            }).ToList();

            var announcement = await _repository.Find(cmd.Id);

            _repository.ResetAssociations(announcement);
            announcement.UpdateAssociations(announcementPoints);
            _repository.UpdateAnnouncement(announcement);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(
                new AnnouncementCreatedOrUpdated(
                    announcement.Id,
                announcement.Title,
                    announcement.Description,
                    announcement.RequestTargetId,
                announcementPoints.Select(m => m.PointId).ToList(),
                false
            ));

            return new UpdateAnnouncementCommandResult()
            {
                Id = announcement.Id,
                Title = announcement.Title
            };
        }
    }
}