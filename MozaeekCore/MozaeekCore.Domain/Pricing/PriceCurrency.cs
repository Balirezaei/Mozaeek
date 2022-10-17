using System.Collections.Generic;

namespace MozaeekCore.Domain.Pricing
{
    public class PriceCurrency : BasicInfo
    {
        protected PriceCurrency() { }

        public PriceCurrency(long id, string unit, string currencyCode)
        {
            Id = id;
            Unit = unit;
            CurrencyCode = currencyCode;
        }

        public string Unit { get; set; }
        public string CurrencyCode { get; set; }
        public virtual ICollection<RequestPriceHeader> RequestPriceHeaders { get; set; }
        public virtual ICollection<SubjectPriceHeader> SubjectPriceHeaders { get; set; }
    }
}