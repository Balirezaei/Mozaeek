namespace MozaeekCore.Specs.Model
{
    public class PreRequest
    {
        public string Title { get; set; }
        public string Summery { get; set; }
    }
    public class PreRequestResult
    {
        public string Title { get; set; }
        public string Summery { get; set; }
        public bool IsProcessed { get; set; }
    }

}