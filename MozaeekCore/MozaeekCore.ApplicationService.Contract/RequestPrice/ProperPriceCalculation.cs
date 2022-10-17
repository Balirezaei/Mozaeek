using System;

namespace MozaeekCore.ApplicationService.Contract
{
    public class ProperPriceCalculation
    {
        public ProperPriceCalculation(int unitPrice, int systemPercent, int technicianPercent, long priceCurrencyId, string unitPriceTitle)
        {
            UnitPrice = unitPrice;
            SystemPercent = systemPercent;
            TechnicianPercent = technicianPercent;
            PriceCurrencyId = priceCurrencyId;
            PriceCurrencyTitle = unitPriceTitle;
            SystemShare = (int)Math.Round(UnitPrice * ((decimal)SystemPercent / 100));
            TechnicianShare = (int)Math.Round(UnitPrice * ((decimal)TechnicianPercent / 100));
        }

        public int UnitPrice { get; private set; }
        public int SystemPercent { get; private set; }
        public int SystemShare { get; private set; }
        public int TechnicianShare { get; private set; }
        public int TechnicianPercent { get; private set; }

        /// <summary>
        /// واحد پول
        /// ریال
        /// تومان
        /// بیت کوین
        /// </summary>
        public long PriceCurrencyId { get; private set; }
        public string PriceCurrencyTitle { get; set; }
        public string ProperTitle { get; set; }
    }
}