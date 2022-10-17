using System;
using MozaeekUserProfile.Common;

namespace MozaeekUserProfile.Domain
{
    public class UserDashboard
    {
        protected UserDashboard()
        {

        }

        public UserDashboard(long id, EntityType type, long userId, string title, long entityId)
        {
            Id = id;
            EntityType = type;
            EntityTypeDescription = type.ToString();
            UserId = userId;
            Title = title;
            EntityId = entityId;
            //DashboardType = dashboardType;
            CreateDate = DateTime.Now;
        }

        public long Id { get; private set; }
        public EntityType EntityType { get; private set; }
        public long EntityId { get; set; }
        public string EntityTypeDescription { get; set; }
        public long UserId { get; private set; }
        public virtual User User { get; private set; }
        public string Title { get; private set; }
        public DateTime CreateDate { get; private set; }
        //public DashboardType DashboardType { get; private set; }

    }
}