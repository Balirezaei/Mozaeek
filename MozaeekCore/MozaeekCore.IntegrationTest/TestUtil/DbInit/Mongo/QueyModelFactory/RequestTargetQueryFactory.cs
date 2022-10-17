using System;
using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class RequestTargetQueryFactory
    {
        private SubjectQueryFactory _subjectQueryFactory;
        private LabelQueryFactory _labelQueryFactory;

        public RequestTargetQueryFactory(SubjectQueryFactory subjectQueryFactory, LabelQueryFactory labelQueryFactory)
        {
            _subjectQueryFactory = subjectQueryFactory;
            _labelQueryFactory = labelQueryFactory;
        }

        public RequestTargetQuery WithFirstLevel_Independent_Subject()
        {
            return new RequestTargetQuery(1, "کارت بازرگانی","", new List<LabelQuery>(){ _labelQueryFactory.GetFirstLevel() }, new List<SubjectQuery>() { _subjectQueryFactory.FirstLevelSubjectQuery(), _subjectQueryFactory.IndependentSubjectQuery() },  DateTime.Now, Guid.NewGuid(), false);

        }

        public RequestTargetQuery WithSecondLevelSubject()
        {
            return new RequestTargetQuery(2, "زمینه اسلام", "", new List<LabelQuery>() { _labelQueryFactory.GetSecondLevelNo0() }, new List<SubjectQuery>() { _subjectQueryFactory.SecondLevelNo1SubjectQuery() },  DateTime.Now, Guid.NewGuid(), false);
        }

        public RequestTargetQuery WithSecondLevelNo2Subject()
        {
            return new RequestTargetQuery(3,
                "زمینه مسیحی", "", null, new List<SubjectQuery>() { _subjectQueryFactory.SecondLevelNo2SubjectQuery() },  DateTime.Now, Guid.NewGuid(), false);
        }

        public RequestTargetQuery WithThirdLevelSubject()
        {
            return new RequestTargetQuery(4,
                  "زمینه شیعه", "", null, new List<SubjectQuery>() { _subjectQueryFactory.ThirdLevelSubjectQuery() },  DateTime.Now, Guid.NewGuid(), false);
        }
        public RequestTargetQuery WithLastLevelSubject()
        {
            return new RequestTargetQuery(5,
                "RequestTarget زمینه زیر مجموعه شیعه", "", null, new List<SubjectQuery>() { _subjectQueryFactory.LastLevelSubjectQuery() },  DateTime.Now, Guid.NewGuid(), false);
        }

    }
}