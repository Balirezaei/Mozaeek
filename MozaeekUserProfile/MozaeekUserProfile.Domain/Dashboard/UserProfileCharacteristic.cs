using System.Collections.Generic;

namespace MozaeekUserProfile.Domain
{
    public class UserProfileCharacteristic
    {
        public int Id { get; private set; }
        public int UserProfileCharacteristicOwnerId { get; private set; }
        public virtual UserProfileCharacteristicOwner UserProfileCharacteristicOwner { get; private set; }
        public virtual ICollection<UserDashboardCharacteristic> UserDashboardCharacteristics { get; set; }
        public string FirstLabelParentTitle { get; private set; }
        public long FirstLabelParentId { get; private set; }
        public long LabelId { get; private set; }
        public string LabelTitle { get; private set; }

        protected UserProfileCharacteristic()
        {
        }

        public UserProfileCharacteristic(UserProfileCharacteristicOwner userCharacteristicOwner, long labelId, string labelTitle, string firstLabelParentTitle, long firstLabelParentId)
        {
            UserProfileCharacteristicOwnerId = userCharacteristicOwner.Id;
            LabelId = labelId;
            LabelTitle = labelTitle;
            FirstLabelParentTitle = firstLabelParentTitle;
            FirstLabelParentId = firstLabelParentId;
        }
    }
}