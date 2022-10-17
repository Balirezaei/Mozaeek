namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserDashboardCharacteristicInput
    {
        public long UserId { get; set; }
        public int[] CharacteristicIds { get; set; }

    }
}