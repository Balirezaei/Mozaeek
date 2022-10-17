using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RreadModel.Mongo.Test.Builder
{
    public class RequestBuilder
    {
        public RequestParameter BuildSampleParameter()
        {
            return new RequestParameter(1, 1, 1,
                new List<RequestQueryDependency>() { new RequestQueryDependency("test", 1) },
               // new List<RequestQueryDependency>() { new RequestQueryDependency("test", 1) },
                new List<RequestQueryDependency>() { new RequestQueryDependency("test", 1) },
                new List<RequestQueryDependency>()
                {
                    new RequestQueryDependency("test", 1)
                }, new List<long>()
           , true, "summary","regulation",null,null,null);
        }
    }
}