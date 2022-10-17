using System;
using System.Collections.Generic;
using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Exceptions;

namespace MozaeekCore.Domain.Pricing
{
    public class RequestPriceHeader
    {
        public long Id { get; private set; }
        public PriceUnit PriceUnit { get; private set; }
        public int PriceUnitId { get; private set; }
        public long PriceAmount { get; private set; }
        public string Title { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<RequestPriceDetail> PriceDetails { get; private set; }

        public RequestPriceHeader(long id, string title, int priceUnitId, long priceAmount, DateTime startDate, DateTime? endDate, ICollection<RequestPriceDetail> priceDetails)
        {
            Id = id;
            Title = title.Recheck();
            PriceUnitId = priceUnitId;
            PriceAmount = priceAmount;
            if (endDate != null && endDate < startDate)
            {
                throw new UserFriendlyException("بازه زمانی انتخابی صحیح نمی باشد.");
            }
            if (endDate != null && endDate < DateTime.Now)
            {
                throw new UserFriendlyException("بازه زمانی انتخابی صحیح نمی باشد.");
            }
            StartDate = startDate;
            EndDate = endDate;
            IsActive = true;
            PriceDetails = priceDetails;
        }

        public void DeActivePricing()
        {
            IsActive = false;
            EndDate = DateTime.Now;
        }

        public void ResetDetails()
        {
            this.PriceDetails.Clear();
        }

        public void AddDetails(List<RequestPriceDetail> details)
        {
            this.PriceDetails = details;
        }

        public void Update(string title, in DateTime startDate, DateTime? endDate, in int priceUnitId, in long priceAmount)
        {
            if (endDate != null && endDate < startDate)
            {
                throw new UserFriendlyException("بازه زمانی انتخابی صحیح نمی باشد.");
            }
            if (endDate != null && endDate < DateTime.Now)
            {
                throw new UserFriendlyException("بازه زمانی انتخابی صحیح نمی باشد.");
            }
            Title = title.Recheck();
            EndDate = endDate;
            StartDate = startDate;
            PriceUnitId = priceUnitId;
            PriceAmount = priceAmount;
        }
    }
}