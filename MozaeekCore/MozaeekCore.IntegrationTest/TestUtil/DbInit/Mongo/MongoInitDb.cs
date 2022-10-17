using System;
using System.Collections.Generic;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.IntegrationTest.TestUtil.DbInit
{
    public class MongoInitDb
    {
        private readonly MongoRepository _mongoRepository;
        private readonly SubjectQueryFactory _subjectQueryFactory;
        private readonly RequestTargetQueryFactory _requestTargetQueryFactory;
        private readonly AnnouncementQueryFactory _announcementQueryFactory;
        private readonly LabelQueryFactory _labelQueryFactory;
        private readonly PointQueryFactory _pointQueryFactory;
        private readonly RequestActQueryFactory _requestActQueryFactory;
        private readonly RequestQueryFactory _requestQueryFactory;
        public MongoInitDb(MongoRepository mongoRepository, SubjectQueryFactory subjectQueryFactory, RequestTargetQueryFactory requestTargetQueryFactory, AnnouncementQueryFactory announcementQueryFactory, LabelQueryFactory labelQueryFactory, PointQueryFactory pointQueryFactory, RequestActQueryFactory requestActQueryFactory, RequestQueryFactory requestQueryFactory)
        {
            _mongoRepository = mongoRepository;
            _subjectQueryFactory = subjectQueryFactory;
            _requestTargetQueryFactory = requestTargetQueryFactory;
            _announcementQueryFactory = announcementQueryFactory;
            _labelQueryFactory = labelQueryFactory;
            _pointQueryFactory = pointQueryFactory;
            _requestActQueryFactory = requestActQueryFactory;
            _requestQueryFactory = requestQueryFactory;
        }

        public void InitDb()
        {
            _mongoRepository.LabelQueryCollection.InsertMany(new []{ _labelQueryFactory.GetFirstLevel(), _labelQueryFactory.GetSecondLevelNo0(), _labelQueryFactory.GetSecondLevelNo1() });
            _mongoRepository.SubjectQueryCollection.InsertMany(new[] { _subjectQueryFactory.FirstLevelSubjectQuery(), _subjectQueryFactory.SecondLevelNo1SubjectQuery(), _subjectQueryFactory.SecondLevelNo2SubjectQuery(), _subjectQueryFactory.ThirdLevelSubjectQuery(), _subjectQueryFactory.LastLevelSubjectQuery(), _subjectQueryFactory.IndependentSubjectQuery() });


            _mongoRepository.PointQueryCollection.InsertMany(new[] { _pointQueryFactory.GetFirstLevelPointQuery(), _pointQueryFactory.GetSecondLevelPointQuery(), _pointQueryFactory.GetThirdSecondPointQuery() });

            var firstTargetWithSubject = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();
            var secondTargetWithSubject = _requestTargetQueryFactory.WithSecondLevelSubject();
            var thirdTargetWithSubject = _requestTargetQueryFactory.WithSecondLevelNo2Subject();
            var fourthTargetWithSubject = _requestTargetQueryFactory.WithThirdLevelSubject();
            var fifthTargetWithSubject = _requestTargetQueryFactory.WithLastLevelSubject();

            _mongoRepository.RequestTargetQueryCollection.InsertMany(new[] { firstTargetWithSubject, secondTargetWithSubject, thirdTargetWithSubject, fourthTargetWithSubject, fifthTargetWithSubject });
            
            //var firstAnnouncement = _announcementQueryFactory.AnnouncementWithTargetWithFirstSubject();
            //var secondAnnouncement = _announcementQueryFactory.AnnouncementWithTargetWithSecondLevelSubject();
            //var thirdAnnouncement = _announcementQueryFactory.AnnouncementNo2WithTargetWithSecondLevelSubject();
            //var fourthAnnouncement = _announcementQueryFactory.AnnouncementWithTargetWithThirdLevelSubject();
            //var fifthAnnouncement = _announcementQueryFactory.AnnouncementWithTargetWithLastLevelSubject();
            //_mongoRepository.AnnouncementQueryCollection.InsertMany(new[] { firstAnnouncement, secondAnnouncement, thirdAnnouncement, fourthAnnouncement, fifthAnnouncement });

            _mongoRepository.RequestActQueryCollection.InsertMany(new []{ _requestActQueryFactory.GetFirstRequestAct(), _requestActQueryFactory.GetSecondRequestAct() });
            _mongoRepository.RequestQueryCollection.InsertMany(new[]
            {
                _requestQueryFactory.FirstRequestQueryWithFirstTarget(),
                _requestQueryFactory.SecondRequestQueryWithFirstTarget()
            });
        }

        public void DropDb()
        {
            _mongoRepository.RemoveDB();
        }

    }
}