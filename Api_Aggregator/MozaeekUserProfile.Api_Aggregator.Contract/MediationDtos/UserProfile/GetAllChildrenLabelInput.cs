namespace Api_Aggregator.Contract.MediationDtos
{
    public class GetAllChildrenLabelInput
    {
        public long UserId { get; set; }
        public int OwnerId { get; set; }
        public long FirstNodeId { get; set; }
        public long ParentId { get; set; }
    }
}