using System;
using System.Collections;
using System.Collections.Generic;

namespace Karmizban.Support.Domain
{
    public class TechnicianSuggestedSupport
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<TechnicianRequestSupport> TechnicianRequestSupports { get; set; }


    }
}