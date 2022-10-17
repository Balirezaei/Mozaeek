using System;
using System.Collections.Generic;

namespace MozaeekCore.QueryModel
{
    public class RequestTargetQuery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<LabelQuery> LabelList { get; set; }
        public List<SubjectQuery> SubjectList { get; set; }
        public List<RequestOrgQuery> RequestOrgList { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }

        public RequestTargetQuery(long id, string title, List<LabelQuery> labelList, List<SubjectQuery> subjectList, List<RequestOrgQuery> requestOrgList, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            Title = title;
            LabelList = labelList;
            SubjectList = subjectList;
            RequestOrgList = requestOrgList;
            LastEventId = lastEventId;
            LastEventPublishDate = lastEventPublishDate;
        }
    }

    // public class RequestTargetLabel
    // {
    //     public long Id { get; set; }
    //     public string Title { get; set; }
    //     public long? ParentId { get; set; }
    // }

    public class RequestTargetParameter
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<long> LabelIds { get; set; }
        public List<long> RequestOrgIds { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }

        public RequestTargetParameter(long id, string title, List<long> subjectIds, List<long> labelIds, List<long> requestOrgIds, Guid eventId, DateTime publishEventDate)
        {
            Id = id;
            Title = title;
            SubjectIds = subjectIds;
            LabelIds = labelIds;
            RequestOrgIds = requestOrgIds;
            EventId = eventId;
            PublishEventDate = publishEventDate;
        }
    }

}