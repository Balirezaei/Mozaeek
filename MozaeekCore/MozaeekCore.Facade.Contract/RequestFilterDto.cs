namespace MozaeekCore.Facade.Contract
{
    public class RequestFilterDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
    }
}