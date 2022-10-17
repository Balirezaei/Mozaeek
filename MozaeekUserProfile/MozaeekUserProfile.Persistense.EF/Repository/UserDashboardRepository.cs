using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class UserDashboardRepository : IUserDashboardRepository
    {
        private readonly MozaeekUserProfileContext _dbContext;

        public UserDashboardRepository(MozaeekUserProfileContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<UserDashboard>> GetAll(long userId)
        {
            return _dbContext.UserDashboards.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task<UserDashboard> CreateDashboard(UserDashboard dashboard)
        {
            _dbContext.UserDashboards.Add(dashboard);
            return dashboard;
        }

        public async Task Remove(long userDashboardId)
        {
            var dashboard=await _dbContext.UserDashboards.FindAsync(userDashboardId);
            _dbContext.UserDashboards.Remove(dashboard);
        }
    }
}