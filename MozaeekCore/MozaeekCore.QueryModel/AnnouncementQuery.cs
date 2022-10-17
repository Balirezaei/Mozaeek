using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MozaeekCore.QueryModel
{
    public class AnnouncementQuery : BaseQuery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public List<LabelQuery> LabelList { get; set; }
        public List<RequestOrgQuery> RequestOrgList { get; set; }
        public List<SubjectQuery> SubjectList { get; set; }
        public IList<PointQuery> PointList { get; set; }
        public string ImageUrl { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime ReleaseDate { get; set; }
        public bool HasRequest { get; set; }
        public RequestQuery RequestQuery { get; set; }

        public AnnouncementQuery(long id, string title, string description, IList<PointQuery> pointList,
            List<LabelQuery> labelList, List<SubjectQuery> subjectList, List<RequestOrgQuery> requestOrgList, string imageUrl, string summary, DateTime releaseDate,
             bool hasRequest, Guid lastEventId, DateTime lastEventPublishDate)
        {
            Id = id;
            Title = title;
            Description = description;
            PointList = pointList;
            LabelList = labelList;
            SubjectList = subjectList;
            RequestOrgList = requestOrgList;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
            ImageUrl = imageUrl;
            ReleaseDate = releaseDate;
            Summary = summary;
            this.HasRequest = hasRequest;
        }
    }

    public class AnnouncementParameter
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public List<long> PointIds { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<long> LabelIds { get; set; }
        public List<long> RequestOrgIds { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool HasRequest { get; set; }
    }

}