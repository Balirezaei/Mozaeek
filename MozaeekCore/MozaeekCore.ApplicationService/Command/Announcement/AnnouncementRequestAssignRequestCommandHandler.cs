using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Command
{
    public class AnnouncementRequestAssignRequestCommandHandler : IBaseAsyncCommandHandler<AnnouncementRequestAssignRequestCommand, Nothing>
    {
        private readonly IAnnouncementQueryService _announcementQuery;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementRequestAssignRequestCommandHandler(IUnitOfWork unitOfWork, IAnnouncementRepository announcementRepository, IAnnouncementQueryService announcementQuery, IRequestRepository requestRepository)
        {
            _unitOfWork = unitOfWork;
            _announcementRepository = announcementRepository;
            _announcementQuery = announcementQuery;
            _requestRepository = requestRepository;
        }

        public async Task<Nothing> HandleAsync(AnnouncementRequestAssignRequestCommand cmd)
        {
            var request = await _requestRepository.FindWithoutAssociation(cmd.RequestId);
            var announcement = await _announcementRepository.Find(cmd.AnnouncementId);
            announcement.AssignRequest(request);
            _announcementRepository.UpdateAnnouncement(announcement);

            await _unitOfWork.CommitAsync();
            return new Nothing();
        }
    }
}