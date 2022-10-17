namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileCharacteristicPreSavedInLabelGroupInput
    {
        public long UserId { get; set; }
        public int OwnerId { get; set; }
        public long FirstLabelNodeId { get; set; }
    }
}