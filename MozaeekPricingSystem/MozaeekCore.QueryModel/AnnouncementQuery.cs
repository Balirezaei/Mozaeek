using System;
using System.Collections.Generic;

namespace MozaeekCore.QueryModel
{
    public class AnnouncementQuery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<PointQuery> PointList { get; set; }
        public RequestTargetQuery RequestTarget { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }

        public AnnouncementQuery(long id, string title, IList<PointQuery> pointList, RequestTargetQuery requestTarget, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            PointList = pointList;
            RequestTarget = requestTarget;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }
    }

    public class AnnouncementParameter
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<long> PointIds { get; set; }
        public long RequestTargetId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
    }

}