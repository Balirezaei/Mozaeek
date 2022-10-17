using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class UserPointRepository : IUserPointRepository
    {
        private readonly MozaeekUserProfileContext _context;

        public UserPointRepository(MozaeekUserProfileContext context)
        {
            _context = context;
        }

        public async Task<UserPoint> CreateUserPoint(UserPoint userPoint)
        {
            var presaved = await _context.UserPoints.FirstOrDefaultAsync(m => m.UserId == userPoint.UserId && m.EndDate == null);
            if (presaved != null && presaved.PointId != userPoint.PointId)
            {
                presaved.EndDate = DateTime.Now;
            }

            if (presaved!=null && presaved.PointId == userPoint.PointId)
            {
                return userPoint;
            }
            await _context.UserPoints.AddAsync(userPoint);
            return userPoint;
        }

        public Task<UserPoint> GetActivePoint(long userId)
        {
            return _context.UserPoints.FirstOrDefaultAsync(m => m.UserId == userId && m.EndDate == null);
        }
    }
}