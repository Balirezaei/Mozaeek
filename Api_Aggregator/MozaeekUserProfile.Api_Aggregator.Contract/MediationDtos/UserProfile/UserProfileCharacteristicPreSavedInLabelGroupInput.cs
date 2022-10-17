namespace Api_Aggregator.Contract.MediationDtos
{
    public class UserProfileCharacteristicPreSavedInLabelGroupInput
    {
        public long UserId { get; set; }
        public long FirstLabelNodeId { get; set; }
        public int OwnerId { get; set; }
    }
}