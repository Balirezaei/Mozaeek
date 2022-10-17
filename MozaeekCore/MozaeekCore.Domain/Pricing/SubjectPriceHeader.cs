﻿using System;
using System.Collections.Generic;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Exceptions;

namespace MozaeekCore.Domain.Pricing
{
    public class SubjectPriceHeader
    {
        protected SubjectPriceHeader() { }
        public long Id { get; private set; }

        public virtual PriceCurrency PriceCurrency { get; private set; }
        public long PriceCurrencyId { get; private set; }

        public int PriceAmount { get; private set; }
        public string Title { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
  
        public ICollection<SubjectPriceDetail> PriceDetails { get; private set; }
        public short SystemShare { get; set; }
        public short TechnicianShare { get; private set; }
        public SubjectPriceHeader(long id, string title, long priceCurrencyId, int priceAmount, short systemShare, DateTime startDate, DateTime? endDate,  ICollection<SubjectPriceDetail> priceDetails)
        {
            Id = id;
            Title = title.Recheck();
            PriceCurrencyId = priceCurrencyId;
            PriceAmount = priceAmount;
            if (endDate != null && endDate < startDate)
            {
                throw new UserFriendlyException("بازه زمانی انتخابی صحیح نمی باشد.");
            }
            if (endDate != null && endDate < DateTime.Now)
            {
                throw new UserFriendlyException("بازه زمانی انتخابی صحیح نمی باشد.");
            }
            if (systemShare > 99)
            {
                throw new UserFriendlyException("درصد سهم موزاییک صحیح نمی باشد.");
            }
            SystemShare = systemShare;
            TechnicianShare = (short)(100 - systemShare);
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

        public void AddDetails(List<SubjectPriceDetail> details)
        {
            this.PriceDetails = details;
        }

        public void Update(string title, DateTime startDate, DateTime? endDate, int priceCurrencyId, int priceAmount, short systemShare)
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
            PriceCurrencyId = priceCurrencyId;
            PriceAmount = priceAmount;
            if (systemShare > 99)
            {
                throw new UserFriendlyException("درصد سهم موزاییک صحیح نمی باشد.");
            }
            SystemShare = systemShare;
            TechnicianShare = (short)(100 - systemShare);
        }

    }
}