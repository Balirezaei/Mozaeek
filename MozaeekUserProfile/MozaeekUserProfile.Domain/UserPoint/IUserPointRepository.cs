using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain
{
    public interface IUserPointRepository
    {
        Task<UserPoint> CreateUserPoint(UserPoint userPoint);
        Task<UserPoint> GetActivePoint(long userId);
    }
}