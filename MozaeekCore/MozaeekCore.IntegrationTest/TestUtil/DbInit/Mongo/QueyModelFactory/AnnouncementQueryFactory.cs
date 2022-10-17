using System;
using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class AnnouncementQueryFactory
    {
        private RequestTargetQueryFactory _requestTargetQueryFactory;
        private readonly PointQueryFactory _pointQueryFactory;

        public AnnouncementQueryFactory(RequestTargetQueryFactory requestTargetQueryFactory, PointQueryFactory pointQueryFactory)
        {
            _requestTargetQueryFactory = requestTargetQueryFactory;
            _pointQueryFactory = pointQueryFactory;
        }

        //public AnnouncementQuery AnnouncementWithTargetWithFirstSubject()
        //{
        //    return new AnnouncementQuery(1, "اعلامیه در خصوص کارت بازرگانی", "", new List<PointQuery>(){ _pointQueryFactory.GetFirstLevelPointQuery() }, null, _requestTargetQueryFactory.WithFirstLevel_Independent_Subject(), "", "summary", DateTime.Now, null, false, Guid.NewGuid(), DateTime.Now);
        //}

        //public AnnouncementQuery AnnouncementWithTargetWithSecondLevelSubject()
        //{
        //    return new AnnouncementQuery(2, "اعلامیه در خصوص اسلام", "", null, new List<PointQuery>() { _pointQueryFactory.GetSecondLevelPointQuery() }, _requestTargetQueryFactory.WithSecondLevelSubject(), "", "summary", DateTime.Now, null, false, Guid.NewGuid(), DateTime.Now);
        //}

        //public AnnouncementQuery AnnouncementNo2WithTargetWithSecondLevelSubject()
        //{
        //    return new AnnouncementQuery(3, "اعلامیه در خصوص مسیحی", "", null, new List<PointQuery>() { _pointQueryFactory.GetThirdSecondPointQuery() }, _requestTargetQueryFactory.WithThirdLevelSubject(), "", "summary", DateTime.Now, null, false, Guid.NewGuid(), DateTime.Now);
        //}
        //public AnnouncementQuery AnnouncementWithTargetWithThirdLevelSubject()
        //{
        //    return new AnnouncementQuery(4, "اعلامیه در خصوص شیعه", "", null, null, _requestTargetQueryFactory.WithThirdLevelSubject(), "", "summary", DateTime.Now, null, false, Guid.NewGuid(), DateTime.Now);
        //}

        //public AnnouncementQuery AnnouncementWithTargetWithLastLevelSubject()
        //{
        //    return new AnnouncementQuery(5, "اعلامیه در خصوص زیر مجموعه شیعه", "", null, null, _requestTargetQueryFactory.WithLastLevelSubject(), "", "summary", DateTime.Now, null, false, Guid.NewGuid(), DateTime.Now);
        //}
    }
}