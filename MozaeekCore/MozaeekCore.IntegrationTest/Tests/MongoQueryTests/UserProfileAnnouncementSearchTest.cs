using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Driver;
using Mozaeek.CR.PublicDto.Dto;
using MozaeekCore.Facade.Query;
using MozaeekCore.IntegrationTest.TestUtil.DbInit;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.RestAPI;
using MozaeekCore.ViewModel;
using Xunit;

namespace MozaeekCore.IntegrationTest.Tests.MongoQueryTests
{
    public class UserProfileAnnouncementSearchTest : IClassFixture<WebFactoryInMongoDb<Startup>>
    {
        private readonly WebFactoryInMongoDb<Startup> _factory;
        private readonly SubjectQueryFactory _subjectQueryFactory;

        public UserProfileAnnouncementSearchTest(WebFactoryInMongoDb<Startup> factory)
        {
            _factory = factory;
            _subjectQueryFactory = (SubjectQueryFactory)_factory.Services.GetService(typeof(SubjectQueryFactory));
        }
        /// <summary>
        /// اعلانی شامل یک زمینه می باشد
        /// جستجو در زمینه ی مورد نظر باید به درستی کار کند
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Announcement_QueryFacade_Should_Return_Correctly_With_Independent_Subject_Input()
        {
            //Setup
            var announcementQueryFacade = (IAnnouncementQueryFacade)_factory.Services.GetService(typeof(IAnnouncementQueryFacade));

            var condidateSubject = _subjectQueryFactory.IndependentSubjectQuery();
            var userDashboard = new AnnouncementUserDashboardDto()
            {
                Subjects = new List<long>() { condidateSubject.Id }
            };

            //Exercise
            var announcements = await announcementQueryFacade.GetUserDashboardAnnouncement(userDashboard);

            //Verification
            announcements.List.Count.Should().Be(1);
            //Teardown
        }

        /// <summary>
        /// اعلانی شامل یک زمینه می باشد
        /// جستجو در زمینه ی مورد نظر باید به زیرمحموعه های آن زمینه را نیز برگرداند
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Announcement_QueryFacade_Should_Retrun_Children_correctly_With_Parent_Subject_Input()
        {
            //Setup
            var announcementQueryFacade = (IAnnouncementQueryFacade)_factory.Services.GetService(typeof(IAnnouncementQueryFacade));

            var condidateSubject = _subjectQueryFactory.FirstLevelSubjectQuery();

            var userDashboard = new AnnouncementUserDashboardDto()
            {
                Subjects = new List<long>() { condidateSubject.Id }
            };
            //Exercise
            var announcements = await announcementQueryFacade.GetUserDashboardAnnouncement(userDashboard);
            //Verification
            announcements.List.Count.Should().Be(5);
            //Teardown

        }

        /// <summary>
        /// اعلانی شامل یک زمینه می باشد
        /// جستجو در زمینه ی مورد نظر نباید والد آن زمینه را  برگرداند
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Announcement_QueryFacade_Should_Not_Retrun_Parent_With_Last_Tree_Node_Subject_Input()
        {
            //Setup
            var announcementQueryFacade = (IAnnouncementQueryFacade)_factory.Services.GetService(typeof(IAnnouncementQueryFacade));
            var condidateSubject = _subjectQueryFactory.LastLevelSubjectQuery();

            var userDashboardChild = new AnnouncementUserDashboardDto()
            {
                Subjects = new List<long>() { condidateSubject.Id }
            };

            var userDashboardParentOfChild = new AnnouncementUserDashboardDto()
            {
                Subjects = new List<long>() { condidateSubject.ParentId.Value }
            };
            //Exercise
            var announcementsChild = await announcementQueryFacade.GetUserDashboardAnnouncement(userDashboardChild);
            var announcementParrnt = await announcementQueryFacade.GetUserDashboardAnnouncement(userDashboardParentOfChild);

            //Verification
            announcementsChild.List.Count.Should().Be(1);
            announcementParrnt.List.Count.Should().BeGreaterThan(1);
            //Teardown

        }



    }
}