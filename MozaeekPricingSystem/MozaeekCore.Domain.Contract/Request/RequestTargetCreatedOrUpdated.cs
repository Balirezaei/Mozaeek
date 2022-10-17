using System;
using System.Collections.Generic;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.BasicInfo
{
    public class RequestTargetCreatedOrUpdated : IEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<long> LabelIds { get; set; }
        public List<long> RequestOrgIds { get; set; }

        public RequestTargetCreatedOrUpdated(long id, string title, List<long> subjectIds, List<long> labelIds, List<long> requestOrgIds, bool isCreated)
        {
            Id = id;
            Title = title;
            IsCreated = isCreated;
            SubjectIds = subjectIds;
            LabelIds = labelIds;
            RequestOrgIds = requestOrgIds;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected RequestTargetCreatedOrUpdated()
        {
        }
    }
}