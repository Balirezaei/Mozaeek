using System;
using System.Collections.Generic;

namespace Karmizban.Support.Domain
{
    public class TechnicianRequestSupport
    {
        public long Id { get; set; }
        public Technician Technician { get; set; }
        public TechnicianSuggestedSupport TechnicianSuggestedSupport { get; set; }
        public long? TechnicianSuggestedSupportId { get; set; }
        public long QuestionId { get; set; }
        public string QuestionCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAnswered { get; set; }
        public virtual ICollection<TechnicianRequestSupportAnswer> TechnicianRequestSupportAnswers { get; set; }
    }
}