using System;
using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestPriceDto
    {
        public long Id { get; set; }
        public long PriceUnitId { get; set; }
        public long PriceAmount { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<RequestPriceDetailDto> PriceDetails { get; set; }
        public short SystemShare { get; set; }
        public short TechnicianShare { get; set; }
    }

    public class RequestPriceDetailDto
    {
        public long RequestId { get; set; }
        public string RequestTitle { get; set; }
        public bool FullOnline { get; set; }
    }
}