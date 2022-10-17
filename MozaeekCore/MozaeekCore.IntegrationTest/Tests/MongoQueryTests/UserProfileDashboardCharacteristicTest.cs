using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.Facade.Query.UserProfile;
using MozaeekCore.IntegrationTest.TestUtil.DbInit;
using MozaeekCore.RestAPI;
using MozaeekCore.ViewModel;
using Xunit;

namespace MozaeekCore.IntegrationTest.Tests.MongoQueryTests
{
    public class UserProfileDashboardCharacteristicTest : IClassFixture<WebFactoryInMongoDb<Startup>>
    {
        private readonly WebFactoryInMongoDb<Startup> _factory;
        private readonly LabelQueryFactory _labelQueryFactory;
        private readonly IUserSearchQueryFacade _userSearchQueryFacade;
        public UserProfileDashboardCharacteristicTest(WebFactoryInMongoDb<Startup> factory)
        {
            _factory = factory;
            _labelQueryFactory = (LabelQueryFactory)_factory.Services.GetService(typeof(LabelQueryFactory));
            _userSearchQueryFacade = (IUserSearchQueryFacade)_factory.Services.GetService(typeof(IUserSearchQueryFacade));
        }


        [Fact]
        public async Task Query_should_have_result_with_child_label_user_selection()
        {
            //Setup
            var condidateLabel = _labelQueryFactory.GetSecondLevelNo0();
            var searchByLabel = new FullUserSearchByUserCharacteristics() { LabelIds =new List<long>(){  condidateLabel.Id }};
            //Exercise
            var result = await _userSearchQueryFacade.FullSearchByUserCharacteristics(searchByLabel);
            //Verification
            result.RequestTargets.List.Count.Should().Be(1);
            result.Announcments.List.Count.Should().Be(1);
        }

        [Fact]
        public async Task Query_should_have_result_with_parent_label_user_selection()
        {
            //Setup
            var condidateLabel = _labelQueryFactory.GetFirstLevel();
            var searchByLabel = new FullUserSearchByUserCharacteristics() { LabelIds = new List<long>() { condidateLabel.Id } };
            //Exercise
            var result = await _userSearchQueryFacade.FullSearchByUserCharacteristics(searchByLabel);
            //Verification
            result.RequestTargets.List.Count.Should().Be(2);
            result.Announcments.List.Count.Should().Be(2);
        }
    }
}