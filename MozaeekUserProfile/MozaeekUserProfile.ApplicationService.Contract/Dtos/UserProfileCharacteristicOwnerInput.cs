namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileCharacteristicOwnerInput
    {
        public string OwnerTitle { get; set; }
        public long UserId { get; set; }
    }
    public class UserProfileCharacteristicOwnerUpdateInput
    {
        public string OwnerTitle { get; set; }
        public long UserId { get; set; }
        public int OwnerId { get; set; }
    }
}