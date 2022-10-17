using System;
using System.Collections.Generic;

namespace MozaeekCore.QueryModel
{
    public class SubjectPriceQuery : BaseQuery
    {
        public long Id { get; set; }
        public string PriceCurrencyTitle { get; set; }
        public long PriceCurrencyId { get; set; }
        public int PriceAmount { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public short SystemShare { get; set; }
        public short TechnicianShare { get; set; }
        public List<SubjectPriceDetailQuery> SubjectPriceDetails { get; set; }
    }
    public class SubjectPriceDetailQuery
    {
        public long SubjectId { get; set; }
        public string SubjectTitle { get; set; }
    }
}