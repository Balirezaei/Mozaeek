using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Mapper
{
    public static class UserPointMapper
    {
        public static UserPointInputDto GetUserPointInputDto(this UserPoint userPoint)
        {
            return new UserPointInputDto()
            {
                PointId = userPoint.PointId,
                PointTitle = userPoint.Title,
                UserId = userPoint.UserId
            };
        }
    }
}