using System.Collections.Generic;
using MozaeekUserProfile.Common.ExtensionMethod;

namespace MozaeekUserProfile.Domain
{
    public class UserProfileCharacteristicOwner
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<UserProfileCharacteristic> UserProfileCharacteristics { get; set; }
        public long UserId { get; private set; }
        public virtual User User { get; private set; }

        protected UserProfileCharacteristicOwner()
        {
        }

        public UserProfileCharacteristicOwner(string name, long userId)
        {
            Name = name.Recheck();
            UserId = userId;
        }

        public void UpdateOwnerTitle(string name)
        {
            Name = name.Recheck();
        }
    }
}