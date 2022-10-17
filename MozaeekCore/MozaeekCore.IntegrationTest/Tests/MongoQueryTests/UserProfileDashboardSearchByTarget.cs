using System.Threading.Tasks;
using FluentAssertions;
using MozaeekCore.Facade.Query.UserProfile;
using MozaeekCore.IntegrationTest.TestUtil.DbInit;
using MozaeekCore.RestAPI;
using MozaeekCore.ViewModel;
using Xunit;

namespace MozaeekCore.IntegrationTest.Tests.MongoQueryTests
{
    public class UserProfileDashboardSearchByTargetTests : IClassFixture<WebFactoryInMongoDb<Startup>>
    {
        private readonly WebFactoryInMongoDb<Startup> _factory;
        private readonly RequestTargetQueryFactory _requestTargetQueryFactory;
        private readonly IUserSearchQueryFacade _userSearchQueryFacade;

        public UserProfileDashboardSearchByTargetTests(WebFactoryInMongoDb<Startup> factory)
        {
            _factory = factory;
            _requestTargetQueryFactory = (RequestTargetQueryFactory)_factory.Services.GetService(typeof(RequestTargetQueryFactory));
            _userSearchQueryFacade = (IUserSearchQueryFacade)_factory.Services.GetService(typeof(IUserSearchQueryFacade));
        }

        /// <summary>
        /// نشان برابر کارت بازرگانی
        /// یک اعلامیه به صورت فیک ثبت شده
        /// یک نشان هم مطابق به همان نشانی که کاربر انتخاب کرده به عنوان نتیجه نشان داده میشود
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UserSearchQueryFacade_Should_have_result_with_correspond_Target_and_No_Request_input()
        {
            //Setup
            var condidateTarget = _requestTargetQueryFactory.WithFirstLevel_Independent_Subject();

            //Exercise
            var result = await _userSearchQueryFacade.FullUserSearchByRequestTarget(new FullUserSearchByRequestTarget()
            {
                RequestTargetId = condidateTarget.Id
            });
            
            //Verification
            result.Announcments.List.Count.Should().Be(1);
            result.RequestTargets.List.Count.Should().Be(1);
        }

    }
}