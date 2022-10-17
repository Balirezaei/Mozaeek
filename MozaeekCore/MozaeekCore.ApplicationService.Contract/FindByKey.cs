using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Base;
using System.Collections.Generic;
namespace MozaeekCore.ApplicationService.Contract
{
    public class FindByKey : Query
    {
        public FindByKey(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }

    public class FindByKeyEditMode : Query
    {
        public FindByKeyEditMode(long? id)
        {
            Id = id;
        }

        public long? Id { get; set; }
    }

    public class Nothing : Query
    {

    }

    public class FindByTextSearch : Query
    {
        public FindByTextSearch(string query)
        {
            Query = query.Recheck();
        }

        public string Query { get; set; }
    }
    public class FindByListKey : Query
    {
        public FindByListKey(List<long> ids)
        {
            Ids = ids;
        }

        public List<long> Ids { get; set; }
    }

    // public class TotalCount
    // {
    //     public long Count { get;private set; }
    //
    //     public TotalCount(long count)
    //     {
    //         Count = count;
    //     }
    // }

}