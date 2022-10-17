using System;
using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateSubjectPriceCommand : Command
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<long> SubjectIds { get; set; }
        public int PriceAmount { get; set; }
        public int PriceUnitId { get; set; }
        public short SystemShare { get; set; }
    }
}