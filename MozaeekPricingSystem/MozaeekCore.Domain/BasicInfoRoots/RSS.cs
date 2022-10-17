using MozaeekCore.Common.ExtentionMethod;

namespace MozaeekCore.Domain
{
    public class RSS : BasicInfo
    {
        public RSS(long id, string url, string source, int intervalDataReceiveHours)
        {
            Id = id;
            Url = url;
            Source = source.Recheck();
            IntervalDataReceiveHours = intervalDataReceiveHours;
            IsActive = true;
        }

        public long Id { get; private set; }
        public string Url { get; private set; }
        public bool IsActive { get; private set; }
        public string Source { get; private set; }
        public int IntervalDataReceiveHours { get; private set; }

        public void Update(string url, string source, bool isActive, int intervalDataReceiveHours)
        {
            IsActive = isActive;
            Source = source.Recheck();
            Url = url;
        }

    }

}