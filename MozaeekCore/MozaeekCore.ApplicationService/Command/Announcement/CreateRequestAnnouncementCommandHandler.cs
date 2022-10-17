using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    // public class CreateRequestAnnouncementCommandHandler : IBaseAsyncCommandHandler<CreateRequestAnnouncementCommand, CreateAnnouncementCommandResult>
    // {
    //     private readonly IUnitOfWork _unitOfWork;
    //     private readonly IMessagePublisher _publisher;
    //     private readonly IAnnouncementRepository _repository;
    //     private readonly IFileRepository _fileRepository;
    //
    //     public CreateRequestAnnouncementCommandHandler(IUnitOfWork unitOfWork, IMessagePublisher publisher, IAnnouncementRepository repository, IFileRepository fileRepository)
    //     {
    //         _unitOfWork = unitOfWork;
    //         _publisher = publisher;
    //         _repository = repository;
    //         _fileRepository = fileRepository;
    //     }
    //     public async Task<CreateAnnouncementCommandResult> HandleAsync(CreateRequestAnnouncementCommand cmd)
    //     {
    //         var announcementPoints = (cmd.Points ?? new List<long>()).Select(m => new AnnouncementPoint()
    //         {
    //             PointId = m
    //         }).ToList();
    //
    //         var announcement = new Announcement(0, cmd.Title, cmd.Description, cmd.Summary, cmd.RequestTargetId, announcementPoints,cmd.RequestId, cmd.FileId);
    //         string filePath = "";
    //         string fileUrl = "";
    //         await _repository.CreatAnnouncement(announcement);
    //         _repository.UpdateNewsRssState(cmd.NewsId);
    //         await _unitOfWork.CommitAsync();
    //         await _publisher.PublishAsync(new AnnouncementCreatedOrUpdated(announcement.Id, announcement.Title, announcement.Description, announcement.RequestTargetId, announcementPoints.Select(z => z.PointId).ToList(), announcement.Summary, cmd.Url, announcement.ReleaseDate, true));
    //
    //         return new CreateAnnouncementCommandResult()
    //         {
    //             Id = announcement.Id,
    //             Title = announcement.Title
    //         };
    //     }
    // }
}