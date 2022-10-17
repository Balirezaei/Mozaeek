using System.Collections.Generic;

namespace Api_Aggregator.Contract.MediationDtos.MozaeekCore.Common
{
    public class ListResult<T>//: BaseResult
    {
        public List<T> List { get; set; }
    }
}
