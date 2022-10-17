using System.Collections.Generic;

namespace MozaeekTechnicianProfile.ApiCall.ResponseMessages
{
    public class ListResult<T>//: BaseResult
    {
        public List<T> List { get; set; }
    }
}
