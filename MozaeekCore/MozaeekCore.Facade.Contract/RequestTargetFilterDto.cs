namespace MozaeekCore.Facade.Contract
{
    public class RequestTargetFilterDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
    }
}