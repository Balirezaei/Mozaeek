using System;
using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class RequestQueryFactory
    {
        private readonly RequestTargetQueryFactory _requestTargetQueryFactory;
        private readonly PointQueryFactory _pointQueryFactory;
        private readonly RequestActQueryFactory _requestActQueryFactory;
        public RequestQueryFactory(RequestTargetQueryFactory requestTargetQueryFactory, PointQueryFactory pointQueryFactory, RequestActQueryFactory requestActQueryFactory)
        {
            _requestTargetQueryFactory = requestTargetQueryFactory;
            _pointQueryFactory = pointQueryFactory;
            _requestActQueryFactory = requestActQueryFactory;
        }

        public RequestQuery FirstRequestQueryWithFirstTarget()
        {
            var target = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();
            var act = _requestActQueryFactory.GetFirstRequestAct();
            var title = act.Title + " " + target.Title;
       
            var necessities = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Necessity 1",1),
                new RequestQueryDependency("Necessity 2",2),
            };
            var actions = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Action 1",1),
                new RequestQueryDependency("Action 2",2),
            };
            var qualifications = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Qualification 1",1),
                new RequestQueryDependency("Qualification 2",2),
            };
            return new RequestQuery(1, title, act, target,  necessities, actions, qualifications,
                new List<PointQuery>() { _pointQueryFactory.GetFirstLevelPointQuery() },
                null,
                null,
                null, DateTime.Now, Guid.NewGuid(),
                false, "description", "regulation", null, null);
        }
        public RequestQuery SecondRequestQueryWithFirstTarget()
        {
            var target = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();
            var act = _requestActQueryFactory.GetFirstRequestAct();
            var title = act.Title + " " + target.Title;
            var documents = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Document 1",1),
                new RequestQueryDependency("Document 2",2),
            };
            var necessities = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Necessity 1",1),
                new RequestQueryDependency("Necessity 2",2),
            };
            var actions = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Action 1",1),
                new RequestQueryDependency("Action 2",2),
            };
            var qualifications = new List<RequestQueryDependency>()
            {
                new RequestQueryDependency("Qualification 1",1),
                new RequestQueryDependency("Qualification 2",2),
            };
            return new RequestQuery(2, title, act, target,  necessities, actions, qualifications,
                new List<PointQuery>() { _pointQueryFactory.GetSecondLevelPointQuery() },
                null,
                null,
                null, DateTime.Now, Guid.NewGuid(),
                false, "description", "regulation", null, null);
        }
    }
}