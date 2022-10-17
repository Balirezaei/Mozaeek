using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitRequestPriceDto
    {
        public List<UnitPriceDto> UnitPrices { get; set; }
        public List<RequestGrid> RequestList { get; set; }
        
    }
}