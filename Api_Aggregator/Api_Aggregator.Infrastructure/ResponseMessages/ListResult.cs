using System.Collections.Generic;

namespace Api_Aggregator.Infrastructure.ResponseMessages
{
    public class ListResult<T>//: BaseResult
    {
        public List<T> List { get; set; }
    }
}
