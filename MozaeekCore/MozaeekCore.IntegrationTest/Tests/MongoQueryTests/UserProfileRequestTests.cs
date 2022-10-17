using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.Facade.Query.UserProfile;
using MozaeekCore.IntegrationTest.TestUtil.DbInit;
using MozaeekCore.RestAPI;
using Xunit;

namespace MozaeekCore.IntegrationTest.Tests.MongoQueryTests
{
    public class UserProfileRequestTests : IClassFixture<WebFactoryInMongoDb<Startup>>
    {
        private readonly WebFactoryInMongoDb<Startup> _factory;
        private readonly RequestTargetQueryFactory _requestTargetQueryFactory;
        private readonly RequestQueryFactory _requestQueryFactory;
        private readonly PointQueryFactory _pointQueryFactory;
        private readonly IUserSearchQueryFacade _userSearchQueryFacade;

        public UserProfileRequestTests(WebFactoryInMongoDb<Startup> factory)
        {
            _factory = factory;
            _requestTargetQueryFactory = (RequestTargetQueryFactory)_factory.Services.GetService(typeof(RequestTargetQueryFactory));
            _requestQueryFactory = (RequestQueryFactory)_factory.Services.GetService(typeof(RequestQueryFactory));
            _pointQueryFactory = (PointQueryFactory)_factory.Services.GetService(typeof(PointQueryFactory));
            _userSearchQueryFacade = (IUserSearchQueryFacade)_factory.Services.GetService(typeof(IUserSearchQueryFacade));
        }

        [Fact]
        public async Task Requests_with_same_Target_and_diffrent_Point_should_have_currect_search_result()
        {
            var target = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();

            //exercise
            var result = await _userSearchQueryFacade.GetUserRequestByRequestTargetId(target.Id);

            //Verification
            result.Points.Count.Should().Be(2);
        }


        [Fact]
        public async Task Requests_with_same_Target_and_specefic_Point_should_have_currect_search_result()
        {
            //Setup
            var target = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();
            var request = _requestQueryFactory.FirstRequestQueryWithFirstTarget();
            var pointId = request.Points.First().Id;
            var actId = request.RequestAct.Id;

            //exercise
            var result = await _userSearchQueryFacade.GetUserRequestByRequestTargetId(target.Id, pointId, actId);

            //Verification
            result.Title.Should().Be(request.RequestTarget.Title);
        }

        [Fact]
        public async Task Requests_with_same_Target_and_incorrect_Point_should_not_have_result()
        {
            //Setup
            var target = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();
            var request = _requestQueryFactory.FirstRequestQueryWithFirstTarget();
            var pointNotInRequest = _pointQueryFactory.GetThirdSecondPointQuery();
            var actId = request.RequestAct.Id;

            //exercise
            var result = await _userSearchQueryFacade.GetUserRequestByRequestTargetId(target.Id, pointNotInRequest.Id, actId);

            //Verification
            result.Should().BeNull();
        }
    }
}