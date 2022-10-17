namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileCharacteristicInput
    {
        public int OwnerId { get; set; }
        public string OwnerTitle { get; set; }
        public string LabelTitle { get; set; }
        public long LabelId { get; set; }
        public long UserId { get; set; }
        public string FirstLabelParentTitle { get; set; }
        public long FirstLabelParentId { get; set; }
    }
}