namespace MozaeekCore.Specs.Model
{
    public class UnProcessedRequest
    {
        public string Title { get; set; }
        public string Summery { get; set; }
    }
    public class UnProcessedRequestResult
    {
        public string Title { get; set; }
        public string Summery { get; set; }
        public bool IsProcessed { get; set; }
    }

}