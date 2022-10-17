using MozaeekTechnicianProfile.Common;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class UserDashboardDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public EntityType EntityType { get; set; }
        public string EntityTypeDescription { get; set; }
        public long UserId { get; set; }
        public long EntityId { get; set; }
    }
}