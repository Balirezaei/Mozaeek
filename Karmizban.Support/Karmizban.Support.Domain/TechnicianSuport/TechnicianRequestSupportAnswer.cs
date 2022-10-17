using System;

namespace Karmizban.Support.Domain
{
    public class TechnicianRequestSupportAnswer
    {
        public long Id { get; set; }
        public long TechnicianRequestSupportId { get; set; }
        public virtual TechnicianRequestSupport TechnicianRequestSupport { get; set; }
        public string AnswerDescription { get; set; }
        public DateTime CreateDate { get; set; }
    }
}