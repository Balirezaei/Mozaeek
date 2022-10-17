using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateAnnouncementCommandHandler : IBaseAsyncCommandHandler<CreateAnnouncementCommand, CreateAnnouncementCommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        private readonly IAnnouncementRepository _repository;
        private readonly IFileRepository _fileRepository;

        public CreateAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMessagePublisher publisher, IAnnouncementRepository repository, IFileRepository fileRepository)
        {
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _repository = repository;
            _fileRepository = fileRepository;
        }
        public async Task<CreateAnnouncementCommandResult> HandleAsync(CreateAnnouncementCommand cmd)
        {
            var announcementPoints = (cmd.Points ?? new List<long>()).Select(m => new AnnouncementPoint()
            {
                PointId = m
            }).ToList();

            var announcementSubjects = (cmd.Subjects ?? new List<long>()).Select(m => new AnnouncementSubject()
            {
                SubjectId = m
            }).ToList();

            var announcementRequestOrgs = (cmd.RequestOrgs ?? new List<long>()).Select(m => new AnnouncementRequestOrg()
            {
                RequestOrgId = m
            }).ToList();

            var announcementLabels = (cmd.Labels ?? new List<long>()).Select(m => new AnnouncementLabel()
            {
                LabelId = m
            }).ToList();
            var announcement = new Announcement(0, cmd.Title, cmd.Description, cmd.Summary, announcementPoints, cmd.HasRequest, cmd.FileId, announcementSubjects, announcementLabels, announcementRequestOrgs);
            string filePath = "";
            string fileUrl = "";

            await _repository.CreatAnnouncement(announcement);
            await _repository.UpdateNewsRssState(cmd.NewsId);

            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(new AnnouncementCreatedOrUpdated(announcement.Id, announcement.Title, announcement.Description,
                cmd.Subjects, cmd.Labels, cmd.RequestOrgs,
                announcementPoints.Select(z => z.PointId).ToList(), announcement.Summary,
                cmd.Url, announcement.ReleaseDate, announcement.HasRequest, announcement.RequestId, true));

            return new CreateAnnouncementCommandResult()
            {
                Id = announcement.Id,
                Title = announcement.Title
            };
        }
    }
}