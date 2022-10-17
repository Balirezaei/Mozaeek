using MozaeekUserProfile.Common;

namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserDashboardCreateResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public EntityType EntityType { get; set; }
        //public DashboardType DashboardType { get; set; }
    }
}