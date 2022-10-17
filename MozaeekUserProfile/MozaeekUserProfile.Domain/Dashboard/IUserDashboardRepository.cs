using System.Collections.Generic;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain
{
    public interface IUserDashboardRepository
    {
        Task<List<UserDashboard>> GetAll(long userId);
        Task<UserDashboard> CreateDashboard(UserDashboard dashboard);
        Task Remove(long userDashboardId);

    }
}