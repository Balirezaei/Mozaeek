using System;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class PointQueryFactory
    {
        public PointQuery GetFirstLevelPointQuery()
        {
            return new PointQuery(1, "ایران", null, DateTime.Now, Guid.NewGuid());
        }
        public PointQuery GetSecondLevelPointQuery()
        {
            return new PointQuery(2, "تهران", 1, DateTime.Now, Guid.NewGuid());
        }

        public PointQuery GetThirdSecondPointQuery()
        {
            return new PointQuery(3, "تهران", 2, DateTime.Now, Guid.NewGuid());
        }
    }
}