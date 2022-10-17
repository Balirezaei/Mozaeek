using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.ResponseMessages;

namespace MozaeekCore.Facade.Query
{
    public interface ITechnicianDashboardQueryFacade
    {
        Task<PagedListResult<UserAnnouncementDto>> GetAnnouncementByTechnicianType(TechnicianDashboardContract contract);
    }
    public class TechnicianDashboardQueryFacade: ITechnicianDashboardQueryFacade
    {
        public Task<PagedListResult<UserAnnouncementDto>> GetAnnouncementByTechnicianType(TechnicianDashboardContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
}