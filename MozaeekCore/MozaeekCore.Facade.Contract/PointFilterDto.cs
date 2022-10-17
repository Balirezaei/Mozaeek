namespace MozaeekCore.Facade.Contract
{
    public class PointFilterDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public string Title { get; set; }
    }
}