using System;
using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateSubjectPriceCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<long> SubjectIds { get; set; }
        public int PriceAmount { get; set; }
        public int PriceCurrencyId { get; set; }
        public Int16 SystemShare { get; set; }
    }
}