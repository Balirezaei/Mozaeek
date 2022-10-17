using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.ApplicationService.Contract.Dtos;
using MozaeekUserProfile.ApplicationService.Services;
using MozaeekUserProfile.Core.Core.ResponseMessages;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.IntegrationTest.TestUtil.WebFactory;
using MozaeekUserProfile.Persistense.EF;
using MozaeekUserProfile.RestAPI;
using Newtonsoft.Json;
using Xunit;

namespace MozaeekUserProfile.IntegrationTest.Tests
{
    public class UserProfileCharacteristicTest : IClassFixture<WebFactoryInSqlServer<Startup>>
    {
        private readonly WebFactoryInSqlServer<Startup> _factory;
        private readonly MozaeekUserProfileContext Context;
        private String BaseUrl = "/api/UserProfileCharacteristic/";
        public UserProfileCharacteristicTest(WebFactoryInSqlServer<Startup> factory)
        {
            _factory = factory;
            Context = (MozaeekUserProfileContext)_factory.Services.GetService(typeof(MozaeekUserProfileContext));
        }

        [Fact]
        public async Task CharacteristicOwner_Should_Create_Succesfully()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var expectedName = "Owner IntegratedTest";
            var url = $"{BaseUrl}CreateCharacteristicOwner";
            var json = JsonConvert.SerializeObject(new UserProfileCharacteristicOwnerInput() { OwnerTitle = expectedName, UserId = user.Id });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Exercise
            var response = await client.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();
            var saveObject = JsonConvert.DeserializeObject<Result<UserProfileCharacteristicOwnerCreateResult>>(responseText);


            //Verification
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            saveObject.Data.OwnerTitle.Should().Be(expectedName);

            //Teardown
        }

        [Fact]
        public async Task CharacteristicOwner_Should_Check_Repetetive_Name_For_Each_User()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var owner = await Context.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m => m.UserId == user.Id);

            var expectedName = owner.Name;
            var url = $"{BaseUrl}CreateCharacteristicOwner";
            var json = JsonConvert.SerializeObject(new UserProfileCharacteristicOwnerInput() { OwnerTitle = expectedName, UserId = user.Id });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Exercise
            var response = await client.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();
            var saveObject = JsonConvert.DeserializeObject<Result<UserProfileCharacteristicOwnerCreateResult>>(responseText);


            //Verification
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            saveObject.Data.OwnerTitle.Should().Be(expectedName);
            saveObject.Data.Id.Should().Be(owner.Id);

            //Teardown
        }

        [Fact]
        public async Task Characteristic_with_owner_Should_Create_Succesfully()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var owner = await Context.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m => m.UserId == user.Id);

            var url = $"{BaseUrl}CreateCharacteristic";
            var json = JsonConvert.SerializeObject(new UserProfileCharacteristicInput() { UserId = user.Id, LabelId = 1, LabelTitle = "Test", OwnerId = owner.Id });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Exercise
            var response = await client.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();
            var saveObject = JsonConvert.DeserializeObject<Result<UserProfileCharacteristicCreateResult>>(responseText);


            //Verification
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            saveObject.Data.Id.Should().NotBe(0);

            //Teardown
        }

        [Fact]
        public async Task Characteristic_without_ownerId_Should_Create_Succesfully()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var owner = await Context.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m => m.UserId == user.Id);

            var expectedName = "Owner IntegratedTest";
            var url = $"{BaseUrl}CreateCharacteristic";
            var json = JsonConvert.SerializeObject(new UserProfileCharacteristicInput() { UserId = user.Id, LabelId = 1, LabelTitle = "Test", OwnerTitle = (new Random().Next(1, 100)) + "_Owner" });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Exercise
            var response = await client.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();
            var saveObject = JsonConvert.DeserializeObject<Result<UserProfileCharacteristicCreateResult>>(responseText);


            //Verification
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            saveObject.Data.Id.Should().NotBe(0);

            //Teardown
        }


        [Fact]
        public async Task Characteristic_Dashboard_Should_Create_Succesfully()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var owner = await Context.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m => m.UserId == user.Id);
            var characteristic = new UserProfileCharacteristic(owner, 2, "Child 1", "Parent 1", 1);
            Context.UserProfileCharacteristics.Add(characteristic);
            await Context.SaveChangesAsync();

            var expectedName = "شناسه های " + owner.Name;
            var url = $"{BaseUrl}CreateUserDashboardCharacteristic";
            var json = JsonConvert.SerializeObject(new UserDashboardCharacteristicInput()
            {
                UserId = user.Id,
                CharacteristicIds = new int[] { characteristic.Id }
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Exercise
            var response = await client.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();
            var saveObject = JsonConvert.DeserializeObject<Result<UserDashboardCharacteristicCreateResult>>(responseText);


            //Verification
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            saveObject.Data.UserDashboardCharacteristicTitle.Should().Be(expectedName);

            //Teardown
        }

        [Fact]
        public async Task Characteristic_Owner_On_Delete_Should_Behaive_Cascade()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var owner = await Context.UserProfileCharacteristicOwners.FirstOrDefaultAsync(m => m.UserId == user.Id);
            var characteristic = new UserProfileCharacteristic(owner, 2, "Child 1", "Parent 1", 1);
            Context.UserProfileCharacteristics.Add(characteristic);
            await Context.SaveChangesAsync();

            Context.UserDashboardCharacteristics.Add(new UserDashboardCharacteristic(characteristic.Id, user.Id, owner.Name));
            await Context.SaveChangesAsync();
            var url = $"{BaseUrl}DeleteCharacteristicOwner/" + owner.Id;
            //Exercise
            var response = await client.GetAsync(url);


            var first = await Context.UserDashboardCharacteristics.FirstOrDefaultAsync();

            //Verification
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            first.Should().Be(null);

            //Teardown
        }

        /// <summary>
        /// در یک نود از گروه برچسب ها تنها یک مرتبه امکان ثبت وجود دارد
        /// برای مثال کاربر در رده ی دین یا مسیحی می باشد یا مسلمان
        /// انتخاب هر دو امکان پذیر نیست
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Characteristic_On_Each_Label_Node_Should_Save_One_Item()
        {
            //Setup
            var client = _factory.CreateClient();
            var user = await Context.Users.FirstOrDefaultAsync();
            var userCharacteristic = await Context.UserProfileCharacteristics.Include(m => m.UserProfileCharacteristicOwner).FirstOrDefaultAsync();
            var characteristicService = (IUserProfileCharacteristicService)_factory.Services.GetService(typeof(IUserProfileCharacteristicService));
            var expectedCountOfLabelNode = 1;
            var newUserCharacteristicInput = new UserProfileCharacteristicInput
            {
                UserId = userCharacteristic.UserProfileCharacteristicOwner.UserId,
                OwnerId = userCharacteristic.UserProfileCharacteristicOwnerId,
                LabelTitle = userCharacteristic.LabelTitle + " Plus Addition",
                LabelId = userCharacteristic.LabelId + 1,
                FirstLabelParentId = userCharacteristic.FirstLabelParentId,
                FirstLabelParentTitle = userCharacteristic.FirstLabelParentTitle
            };


            //Exercise
            await characteristicService.CreateCharacteristic(newUserCharacteristicInput);



            //Verification
            var count = await Context.UserProfileCharacteristics
                .Where(m => m.FirstLabelParentId == userCharacteristic.FirstLabelParentId).CountAsync();


            count.Should().Be(expectedCountOfLabelNode);

            //Teardown
        }
    }
}