using MozaeekUserProfile.Common;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserDashboardInputDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public EntityType EntityType { get; set; }
        public long UserId { get; set; }
        public long EntityId { get; set; }
        //public DashboardType DashboardType { get; set; }
    }
}