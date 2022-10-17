using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitSubjectPriceDto
    {
        public List<UnitPriceDto> UnitPrices { get; set; }
        public List<SubjectDto> SubjectList { get; set; }
        
    }
}