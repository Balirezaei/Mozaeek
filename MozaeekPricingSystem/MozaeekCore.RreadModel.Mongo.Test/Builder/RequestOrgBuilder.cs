using System;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RreadModel.Mongo.Test.Builder
{
    public class RequestOrgBuilder
    {
        public RequestOrgQuery BuildFirstLevel()
        {
            return new RequestOrgQuery(1, "شهرداری ها", null, DateTime.Now, Guid.NewGuid());
        }
    }
}