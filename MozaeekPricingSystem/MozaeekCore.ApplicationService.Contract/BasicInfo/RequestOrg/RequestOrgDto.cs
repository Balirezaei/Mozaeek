namespace MozaeekCore.ApplicationService.Contract
{
    /// <summary>
    /// خواستگاه
    /// </summary>
    public class RequestOrgDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}