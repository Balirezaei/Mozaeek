using System;
using System.Collections.Generic;
using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.RequestPrice
{
    public class CreateRequestPriceCommand : Command
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<long> RequestIds { get; set; }
        public long PriceAmount { get; set; }
        public int PriceUnitId { get; set; }
    }


    public class CreateRequestPriceResult
    {
        public long Id { get; set; }
    }

    public class UpdateRequestPriceCommand : Command
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<long> RequestIds { get; set; }
        public long PriceAmount { get; set; }
        public int PriceUnitId { get; set; }
    }

    public class UpdateRequestPriceResult
    {
        public long Id { get; set; }
    }
}