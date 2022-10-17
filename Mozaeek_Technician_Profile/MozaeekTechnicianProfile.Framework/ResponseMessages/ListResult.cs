using System.Collections.Generic;

namespace MozaeekTechnicianProfile.Core.Core.ResponseMessages
{
    public class ListResult<T>//: BaseResult
    {
        public List<T> List { get; set; }
    }
}
