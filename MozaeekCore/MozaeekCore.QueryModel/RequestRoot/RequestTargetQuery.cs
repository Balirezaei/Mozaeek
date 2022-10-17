using System;
using System.Collections.Generic;

namespace MozaeekCore.QueryModel
{
    public class RequestTargetQuery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public List<LabelQuery> LabelList { get; set; }
        //public List<RequestOrgQuery> RequestOrgList { get; set; }
        public List<SubjectQuery> SubjectList { get; set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
        public bool IsDocument { get; set; }

        public RequestTargetQuery(long id, string title, string icon, List<LabelQuery> labelList, List<SubjectQuery> subjectList, DateTime lastEventPublishDate, Guid lastEventId, bool isDocument)
        {
            Id = id;
            Title = title;
            Icon = icon;
            LabelList = labelList;
            SubjectList = subjectList;
            //RequestOrgList = requestOrgList;
            LastEventId = lastEventId;
            IsDocument = isDocument;
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
        //public List<long> RequestOrgIds { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public bool IsDocument { get; set; }
        public string Icon { get; set; }
        public RequestTargetParameter(long id, string title,string icon, List<long> subjectIds, List<long> labelIds, bool isDocument, Guid eventId, DateTime publishEventDate)
        {
            Id = id;
            Title = title;
            Icon = icon;
            SubjectIds = subjectIds;
            LabelIds = labelIds;
            //RequestOrgIds = requestOrgIds;
            EventId = eventId;
            PublishEventDate = publishEventDate;
            IsDocument = isDocument;
        }
    }

}