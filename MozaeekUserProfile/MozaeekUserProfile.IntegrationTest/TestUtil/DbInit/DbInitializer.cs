using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Persistense.EF;

namespace MozaeekUserProfile.IntegrationTest.TestUtil.DbInit
{
    public static class DbInitializer
    {
        public static void InitializeTestDatabaseInSQL(this MozaeekUserProfileContext context)
        {
            var user = new User("09124804347");
            context.Users.Add(user);

            context.SaveChanges();
            var owner = new UserProfileCharacteristicOwner("IntegrationTest", user.Id);
            context.UserProfileCharacteristicOwners.Add(owner);
            context.SaveChanges();
            var characteristic = new UserProfileCharacteristic(owner, 2, "child", "Parent", 1);
            context.UserProfileCharacteristics.Add(characteristic);
            context.SaveChanges();

        }
    }
}