using System;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class SubjectQueryFactory
    {
        public SubjectQuery FirstLevelSubjectQuery()
        {
            return new SubjectQuery(1, "دین","", null, DateTime.Now, Guid.NewGuid());
        }
        public SubjectQuery SecondLevelNo1SubjectQuery()
        {
            return new SubjectQuery(2, "اسلام", "", 1, DateTime.Now, Guid.NewGuid());

        }
        public SubjectQuery SecondLevelNo2SubjectQuery()
        {
            return new SubjectQuery(3, "مسیحی", "", 1, DateTime.Now, Guid.NewGuid());
        }
        public SubjectQuery ThirdLevelSubjectQuery()
        {
            return new SubjectQuery(4, "شیعه", "", 2, DateTime.Now, Guid.NewGuid());
        }

        public SubjectQuery LastLevelSubjectQuery()
        {
            return new SubjectQuery(5, "زیرمجموعه شیعه", "", 4, DateTime.Now, Guid.NewGuid());
        }

        public SubjectQuery IndependentSubjectQuery()
        {
            return new SubjectQuery(6, "Independent", "", null, DateTime.Now, Guid.NewGuid());
        }
    }
}