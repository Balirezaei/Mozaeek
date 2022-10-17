using System;

namespace MozaeekUserProfile.Domain
{
    public class UserDashboardCharacteristic
    {
        public int Id { get; private set; }
        public int UserProfileCharacteristicId { get; private set; }
        public virtual UserProfileCharacteristic UserProfileCharacteristic { get; private set; }
        public long UserId { get; private set; }
        public virtual User User { get; private set; }
        public string Title { get; private set; }
        public DateTime CreateDate { get; private set; }

        public UserDashboardCharacteristic(int userProfileCharacteristicId, long userId, string ownertitle)
        {
            UserProfileCharacteristicId = userProfileCharacteristicId;
            UserId = userId;
            Title = "شناسه های " + ownertitle;
            CreateDate = DateTime.Now;
        }

        protected UserDashboardCharacteristic()
        {

        }
    }
}