using System;
using System.Collections.Generic;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract
{
    public class AnnouncementCreatedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<long> LabelIds { get; set; }
        public List<long> RequestOrgIds { get; set; }

        public List<long> PointIds { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string Summary { get; private set; }
        public string ImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public long? RequestId { get; set; }
        public bool HasRequest { get; set; }

        protected AnnouncementCreatedOrUpdated()
        {
        }

        public AnnouncementCreatedOrUpdated(long id, string title, string description, List<long> subjectIds, List<long> labelIds, List<long> requestOrgIds,  List<long> pointIds, string summary, string imageUrl, DateTime releaseDate, bool hasRequest, long? requestId, bool isCreated)
        {
            Id = id;
            Title = title;
            Description = description;
            SubjectIds = subjectIds;
            LabelIds = labelIds;
            RequestOrgIds = requestOrgIds;
            IsCreated = isCreated;
            Summary = summary;
            PointIds = pointIds;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
            ImageUrl = imageUrl;
            ReleaseDate = releaseDate;
            HasRequest = hasRequest;
            RequestId = requestId;
        }
    }

    public class AnnouncementAssignedRequest : IEvent
    {
        public long AnnouncementId { get; set; }
        public long RequestId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public AnnouncementAssignedRequest(long announcementId, long requestId)
        {
            AnnouncementId = announcementId;
            RequestId = requestId;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}