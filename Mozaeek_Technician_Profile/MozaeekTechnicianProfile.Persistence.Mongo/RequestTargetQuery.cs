using System;
using System.Collections.Generic;

namespace MozaeekTechnicianProfile.Persistence.Mongo
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
    }
}