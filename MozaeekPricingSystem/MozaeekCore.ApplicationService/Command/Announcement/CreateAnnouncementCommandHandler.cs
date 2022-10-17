using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateAnnouncementCommandHandler : IBaseAsyncCommandHandler<CreateAnnouncementCommand, CreateAnnouncementCommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        private readonly IAnnouncementRepository _repository;

        public CreateAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMessagePublisher publisher, IAnnouncementRepository repository)
        {
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _repository = repository;
        }
        public async Task<CreateAnnouncementCommandResult> HandleAsync(CreateAnnouncementCommand cmd)
        {
            var announcementPoints = cmd.Points.Select(m => m.Id).Select(m => new AnnouncementPoint()
            {
                PointId = m
            }).ToList();

            var announcement = new Announcement(0, cmd.Title, cmd.Description, cmd.RequestTargetId, announcementPoints);

            await _repository.CreatAnnouncement(announcement);
            _repository.UpdateNewsRssState(cmd.NewsId);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new AnnouncementCreatedOrUpdated(announcement.Id, announcement.Title, announcement.Description, announcement.RequestTargetId, announcementPoints.Select(z => z.PointId).ToList(), true));

            return new CreateAnnouncementCommandResult()
            {
                Id = announcement.Id,
                Title = announcement.Title
            };
        }
    }
}