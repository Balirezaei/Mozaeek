namespace MozaeekCore.ApplicationService.Contract
{
    public class RSSDto
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string Source { get; set; }
        public int IntervalDataReceiveHours { get; set; }
    }
}