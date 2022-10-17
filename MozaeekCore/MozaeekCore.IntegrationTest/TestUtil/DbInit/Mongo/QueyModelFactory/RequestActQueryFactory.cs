using System;
using MozaeekCore.Domain;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class RequestActQueryFactory
    {
        public RequestActQuery GetFirstRequestAct()
        {
            return new RequestActQuery(1,"اخذ",DateTime.Now, Guid.NewGuid());
        }
        public RequestActQuery GetSecondRequestAct()
        {
            return new RequestActQuery(2, "تمدید", DateTime.Now, Guid.NewGuid());
        }
    }
}