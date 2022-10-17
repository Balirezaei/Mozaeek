using System;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestPriceGrid
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }

    }
}