using System;
using System.Collections.Generic;

namespace MozaeekCore.QueryModel
{
    public class RequestPriceQuery : BaseQuery
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
        public List<RequestPriceDetailQuery> RequestPriceDetails { get; set; }
    }

    public class RequestPriceDetailQuery
    {
        public long RequestId { get; set; }
        public string RequestTitle { get; set; }
        public bool FullOnline { get; set; }
    }

}