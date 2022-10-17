using System;
using System.Collections.Generic;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract
{
    public class RequestPriceCreateOrUpdated : IEvent
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
        public List<long> RequestIds { get; set; }
        public bool IsCreated { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        protected RequestPriceCreateOrUpdated()
        {
        }
        public RequestPriceCreateOrUpdated(long id, string priceCurrencyTitle, long priceCurrencyId, int priceAmount, string title, DateTime startDate, DateTime? endDate, bool isActive, short systemShare, short technicianShare, List<long> requestIds, bool isCreated)
        {
            Id = id;
            PriceCurrencyTitle = priceCurrencyTitle;
            PriceCurrencyId = priceCurrencyId;
            PriceAmount = priceAmount;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            SystemShare = systemShare;
            TechnicianShare = technicianShare;
            RequestIds = requestIds;
            IsCreated = isCreated;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
}