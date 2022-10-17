using System.Collections;
using System.Collections.Generic;
using MozaeekUserProfile.Common.ExtensionMethod;

namespace MozaeekUserProfile.Domain
{
    public class User
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string DeviceId { get; set; }
        public virtual ICollection<UserDashboard> UserDashboards { get; set; }
        public virtual ICollection<UserDiscount> UserDiscounts { get; set; }
        public virtual ICollection<UserPoint> UserPoints { get; set; }
        public virtual ICollection<UserWallet> UserWallets { get; set; }
        public virtual ICollection<UserDashboardCharacteristic> UserDashboardCharacteristics { get; set; }
        public virtual ICollection<UserProfileCharacteristicOwner> UserProfileCharacteristicOwners { get; set; }
        public virtual ICollection<UserQuestion> UserQuestions { get; set; }
        public string LastRefreshToken { get; private set; }

        protected User() { }


        public User(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public bool ProfileIsCompleted
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FirstName) ||
                       !string.IsNullOrWhiteSpace(LastName) ||
                       !string.IsNullOrWhiteSpace(Address);
            }
        }

        public void UpdateInformation(string firstName, string lastName, string email, string address)
        {
            Address = address.Recheck();
            Email = email.Recheck();
            FirstName = firstName.Recheck();
            LastName = lastName.Recheck();
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            LastRefreshToken = refreshToken;


        }
        public string FullName()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                return $"{FirstName} {LastName}";
            }

            return "";
        }
        public void UpdateDeviceId(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}