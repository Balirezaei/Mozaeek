using System;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class LabelQueryFactory
    {
        public LabelQuery GetFirstLevel()
        {
            return new LabelQuery(1, "جنسیت", null, DateTime.Now, Guid.NewGuid());
        }
        public LabelQuery GetSecondLevelNo0()
        {
            return new LabelQuery(2, "زن", 1, DateTime.Now, Guid.NewGuid());
        }
        public LabelQuery GetSecondLevelNo1()
        {
            return new LabelQuery(3, "مرد", 1, DateTime.Now, Guid.NewGuid());
        }
    }
}