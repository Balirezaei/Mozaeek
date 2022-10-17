using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.Facade.Query.UserProfile;
using MozaeekCore.IntegrationTest.TestUtil.DbInit;
using MozaeekCore.RestAPI;
using MozaeekCore.ViewModel;
using Xunit;

namespace MozaeekCore.IntegrationTest.Tests.MongoQueryTests
{
    public class UserProfileDashboardSearchBySubjectTests : IClassFixture<WebFactoryInMongoDb<Startup>>
    {
        private readonly WebFactoryInMongoDb<Startup> _factory;
        private readonly RequestTargetQueryFactory _requestTargetQueryFactory;
        private readonly SubjectQueryFactory _subjectQueryFactory;
        private readonly IUserSearchQueryFacade _userSearchQueryFacade;

        public UserProfileDashboardSearchBySubjectTests(WebFactoryInMongoDb<Startup> factory)
        {
            _factory = factory;
            _requestTargetQueryFactory = (RequestTargetQueryFactory)_factory.Services.GetService(typeof(RequestTargetQueryFactory));
            _subjectQueryFactory = (SubjectQueryFactory)_factory.Services.GetService(typeof(SubjectQueryFactory));
            _userSearchQueryFacade = (IUserSearchQueryFacade)_factory.Services.GetService(typeof(IUserSearchQueryFacade));
        }
        [Fact]
        public async Task Query_should_have_result_with_child_subject_user_selection()
        {
            //Setup
            var condidateSubject = _subjectQueryFactory.LastLevelSubjectQuery();
            var searchBySubject = new FullUserSearchBySubject() {SubjectId = condidateSubject.Id};
            //Exercise
            var result = await _userSearchQueryFacade.FullUserSearchBySubject(searchBySubject);
            //Verification
            result.RequestTargets.List.Count.Should().Be(1);
            result.Announcments.List.Count.Should().Be(1);
        }

        [Fact]
        public async Task Query_should_have_result_with_parent_subject_user_selection()
        {
            //Setup
            var condidateSubject = _subjectQueryFactory.FirstLevelSubjectQuery();
            var searchBySubject = new FullUserSearchBySubject() { SubjectId = condidateSubject.Id };
            //Exercise
            var result = await _userSearchQueryFacade.FullUserSearchBySubject(searchBySubject);
            //Verification
            result.RequestTargets.List.Count.Should().Be(5);
            result.Announcments.List.Count.Should().Be(5);
        }

    }
}