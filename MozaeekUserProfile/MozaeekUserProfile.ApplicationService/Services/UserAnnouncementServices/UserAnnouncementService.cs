using MozaeekUserProfile.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Mozaeek.CR.PublicDto.Dto;

namespace MozaeekUserProfile.ApplicationService.Services.UserAnnouncementServices
{
    public interface IUserAnnouncementService
    {
        Task<AnnouncementUserDashboardDto> GetAnnouncementUserDashboard(long userId);
    }
    public class UserAnnouncementService : IUserAnnouncementService
    {
        private readonly IUserDashboardRepository _userDashboardRepository;
        private readonly IUserPointRepository _userPointrepository;
        public UserAnnouncementService(IUserDashboardRepository userDashboardRepository, IUserPointRepository userPointrepository)
        {
            _userDashboardRepository = userDashboardRepository;
            _userPointrepository = userPointrepository;

        }

        public async Task<AnnouncementUserDashboardDto> GetAnnouncementUserDashboard(long userId)
        {
            var dashboardList =await _userDashboardRepository.GetAll(userId);
            var pointList = await _userPointrepository.GetActivePoint(userId);
            if(dashboardList==null)
                throw new System.Exception("No Dashboard For user");

            return new AnnouncementUserDashboardDto() 
            {
            Subjects= dashboardList?.Where(u=>u.EntityType==Common.EntityType.Subject)?.Select(u=>u.EntityId)?.ToList(),
                //RequestOrgs = dashboardList?.Where(u => u.EntityType == Common.EntityType.RequestOrg)?.Select(u => u.EntityId)?.ToList(),
                //Labels= dashboardList?.Where(u => u.EntityType == Common.EntityType.Label)?.Select(u => u.EntityId)?.ToList(),
                //PointId = pointList?.Id??0

            };
        }
    }
}
