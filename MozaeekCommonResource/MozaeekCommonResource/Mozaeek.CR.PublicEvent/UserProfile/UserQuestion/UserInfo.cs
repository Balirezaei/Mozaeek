namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class UserInfo
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserDeviceId { get; set; }

        public UserInfo(long userId, string userFullName, string userDeviceId)
        {
            UserId = userId;
            UserFullName = userFullName;
            UserDeviceId = userDeviceId;
        }
    }
}