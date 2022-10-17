using System;
using System.Collections.Generic;

namespace MozaeekCore.Domain.Pricing
{
    public class SubjectPriceHeader
    {
        public long Id { get; private set; }
        public PriceUnit PriceUnit { get; private set; }
        public long PriceAmount { get; private set; }
        public string Title { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public Subject Subject { get; private set; }
        public long SubjectId { get; private set; }
        public ICollection<SubjectPriceDetail> PriceDetails { get; private set; }

        public SubjectPriceHeader(long id, PriceUnit priceUnit, long priceAmount, DateTime startDate, DateTime? endDate, long requestId, ICollection<SubjectPriceDetail> priceDetails)
        {
            Id = id;
            PriceUnit = priceUnit;
            PriceAmount = priceAmount;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = true;
            SubjectId = requestId;
            PriceDetails = priceDetails;
        }

        public void DeActivePricing()
        {
            IsActive = false;
            EndDate = DateTime.Now;
        }
    }
}