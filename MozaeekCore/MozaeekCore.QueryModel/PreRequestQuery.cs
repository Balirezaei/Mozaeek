using System;

namespace MozaeekCore.QueryModel
{
    public class PreRequestQuery: BaseQuery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreateDateTime { get; set; }
 
    }
}