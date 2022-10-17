using System;
using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class SubjectPriceDto
    {
        public long Id { get; set; }
        public long PriceCurrencyId { get; set; }
        public long PriceAmount { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<SubjectPriceDetailDto> PriceDetails { get; set; }
        public short SystemShare { get; set; }
        public short TechnicianShare { get; set; }
    }

    public class SubjectPriceDetailDto
    {
        public long SubjectId { get; set; }
        public string SubjectTitle { get; set; }
    }
}