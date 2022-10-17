using System;

namespace Mozaeek.CR.PublicDto
{
    public class ProperPriceResult
    {
        public int UnitPrice { get; set; }
        public int SystemPercent { get; set; }
        public int SystemShare { get; set; }
        public int TechnicianShare { get; set; }
        public int TechnicianPercent { get; set; }
        public long PriceCurrencyId { get; set; }
        public string PriceCurrencyTitle { get; set; }
      
    }
}
