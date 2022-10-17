using System.Collections.Generic;
using MozaeekCore.ViewModel;

namespace MozaeekCore.ApplicationService.Contract
{
    public class InitPropertyDto
    {
        public List<DropDownDto> PropertyTypes { get; set; }
        public List<DropDownDto> PropertyDataTypes { get; set; }
    }
}