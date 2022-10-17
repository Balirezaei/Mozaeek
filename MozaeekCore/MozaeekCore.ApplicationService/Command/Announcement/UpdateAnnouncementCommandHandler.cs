using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateAnnouncementCommandHandler : IBaseAsyncCommandHandler<UpdateAnnouncementCommand, UpdateAnnouncementCommandResult>
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        private readonly ICommandBus _commandBus;
        public UpdateAnnouncementCommandHandler(IAnnouncementRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher, ICommandBus commandBus)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _commandBus = commandBus;
        }

        public async Task<UpdateAnnouncementCommandResult> HandleAsync(UpdateAnnouncementCommand cmd)
        {
            var announcementPoints = (cmd.Points ?? new List<long>()).Select(m => new AnnouncementPoint()
            {
                PointId = m
            }).ToList();
            var announcementSubjects = cmd.Subjects.Select(m => m).Select(m => new AnnouncementSubject()
            {
                SubjectId = m
            }).ToList();

            var announcementRequestOrgs = cmd.RequestOrgs.Select(m => m).Select(m => new AnnouncementRequestOrg()
            {
                RequestOrgId = m
            }).ToList();

            var announcementLabels = cmd.Labels.Select(m => m).Select(m => new AnnouncementLabel()
            {
                LabelId = m
            }).ToList();
            var announcement = await _repository.Find(cmd.Id);
            var imagePath = announcement.File?.Url;
            if (!cmd.Url.IsNullOrEmpty())
            {
                await _commandBus.DispatchAsync<DeleteFileCommand, string>(new DeleteFileCommand(announcement.FileId ?? 0));
                imagePath = cmd.Url;
            }

            _repository.ResetAssociations(announcement);

            announcement.Update(cmd.Title, cmd.Description, cmd.Summary, announcementSubjects, announcementLabels, announcementRequestOrgs, cmd.HasRequest, cmd.FileId);

            announcement.UpdateAssociations(announcementPoints);

            _repository.UpdateAnnouncement(announcement);

            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(
                new AnnouncementCreatedOrUpdated(
                    announcement.Id,
                announcement.Title,
                    announcement.Description,
                    cmd.Subjects, cmd.Labels, cmd.RequestOrgs,
                announcementPoints.Select(m => m.PointId).ToList(),
                    announcement.Summary, imagePath, announcement.ReleaseDate, announcement.HasRequest, announcement.RequestId, false));

            return new UpdateAnnouncementCommandResult()
            {
                Id = announcement.Id,
                Title = announcement.Title
            };
        }
    }
}